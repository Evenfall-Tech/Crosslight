using Crosslight.API.Exceptions;
using Crosslight.API.Nodes;
using Crosslight.API.Nodes.Componentization;
using Crosslight.CIL.Utils.ILSpy;
using ICSharpCode.Decompiler.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Crosslight.CIL.Nodes.Visitors.Syntax
{
    public class SyntaxTreeVisitor : AbstractVisitor<SyntaxTree>
    {
        public SyntaxTreeVisitor(VisitContext context) : base(context)
        {

        }

        public override Node Visit(AstNode node)
        {
            if (node == null) throw new NullReferenceException("Passed node was null.");

            try
            {
                if (!(node is SyntaxTree syntaxTree))
                    throw new WrongVisitorException(nameof(SyntaxTreeVisitor), node.GetType().Name);

                return Visit(syntaxTree);
            }
            catch (VisitorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new VisitorException(e);
            }
        }

        public override Node Visit(SyntaxTree node)
        {
            try
            {
                var root = new ModuleNode(Context.Options.ModuleName);
                // TODO: Parse Usings.
                var usings = node.Children.OfType<UsingDeclaration>();
                // TODO: Parse Attributes.
                var assemblyAttributes = node.Children
                    .OfType<AttributeSection>()
                    .Where(s => s.AttributeTarget == "assembly");

                var namespaces = node.Children.OfType<NamespaceDeclaration>();
                List<NamespaceNode> resultingNamespaceNodes = new List<NamespaceNode>();
                foreach (var ns in namespaces)
                {
                    var nsVisitor = Context?.VisitFactory?
                        .GetVisitor(nameof(NamespaceDeclaration)) as ICILVisitor<NamespaceDeclaration>;
                    Node nsNode = ns.AcceptVisitor(nsVisitor);
                    if (!(nsNode is NamespaceNode namespaceNode))
                    {
                        throw new VisitorException($"{nameof(NamespaceDeclaration)} visitor returned {nsNode.Type}.");
                    }
                    resultingNamespaceNodes.Add(namespaceNode);
                }
                if (Context?.Options?.SplitNamespaces == true)
                {
                    resultingNamespaceNodes = SplitAll(resultingNamespaceNodes);
                }
                foreach (var n in resultingNamespaceNodes)
                    root.Namespaces.Add(n);

                var others = node.Children
                    //.Except(usings)
                    //.Except(assemblyAttributes)
                    .Except(namespaces)
                ;
                foreach (var c in others)
                {
                    Node outNode = Context?.VisitFactory?.GetVisitor(c)?.Visit(c);
                    if (outNode != null)
                    {
                        root.Children.Add(outNode);
                    }
                }
                Node returnNode = root;
                if (Context.Options.CreateProject)
                {
                    var project = new ProjectNode(node.GetAssemblyTitle());
                    project.Modules.Add(root);
                    // TODO: set parent, siblings, etc.
                    returnNode = project;
                }
                return returnNode;
            }
            catch (Exception e)
            {
                throw new VisitorException(e);
            }
        }

        public override Node VisitSyntaxTree(SyntaxTree syntaxTree)
        {
            return Visit(syntaxTree);
        }

        private List<NamespaceNode> SplitAll(IEnumerable<NamespaceNode> original)
        {
            LinkedList<NamespaceWrapper> declarations = new LinkedList<NamespaceWrapper>(original.Select(o => new NamespaceWrapper(o)));
            LinkedList<NamespaceWrapper> result = new LinkedList<NamespaceWrapper>();

            while (declarations.Count > 0)
            {
                var commonParent = declarations.Where(d => d.Identifiers[0] == declarations.First.Value.Identifiers[0]).ToArray();
                result.AddLast(Split(commonParent, 0));
                foreach (var w in commonParent)
                    declarations.Remove(w);
            }

            return result.Select(w => w.Node).ToList();
        }

        private NamespaceWrapper Split(IEnumerable<NamespaceWrapper> parent, int level)
        {
            NamespaceWrapper bottom = parent.SingleOrDefault(p => p.Identifiers.Length == level + 1);
            NamespaceNode node;
            NamespaceNode declaration;
            List<NamespaceWrapper> deeper;
            if (bottom != null)
            {
                declaration = new NamespaceNode(bottom.Identifiers[level]);
                node = bottom.Node;

                foreach (var c in node.Namespaces)
                    declaration.Namespaces.Add(c);
                foreach (var c in node.Functions)
                    declaration.Functions.Add(c);
                foreach (var c in node.Types)
                    declaration.Types.Add(c);
                foreach (var c in node.Values)
                    declaration.Values.Add(c);
                foreach (var c in node.Children)
                    declaration.Children.Add(c);
                deeper = parent.Except(new NamespaceWrapper[] { bottom }).ToList();
            }
            else
            {
                NamespaceWrapper first = parent.First();
                declaration = new NamespaceNode(first.Identifiers[level]);
                node = first.Node;
                deeper = parent.ToList();
            }

            while (deeper.Count > 0)
            {
                var commonParent = deeper.Where(d => d.Identifiers[level + 1] == deeper[0].Identifiers[level + 1]).ToArray();
                var split = Split(commonParent, level + 1);
                declaration.Namespaces.Add(split.Node);

                foreach (var w in commonParent)
                    deeper.Remove(w);
            }

            return new NamespaceWrapper(declaration);
        }

        private class NamespaceWrapper
        {
            public NamespaceNode Node { get; }
            public string[] Identifiers { get; }
            public NamespaceWrapper(NamespaceNode node)
            {
                Node = node;
                Identifiers = node.Identifiers.ToArray();
            }
        }
    }
}
