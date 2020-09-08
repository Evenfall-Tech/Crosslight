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
            if (node == null) throw new NullReferenceException("Passed node was null");

            try
            {
                if (!(node is SyntaxTree syntaxTree)) throw new WrongVisitorException(typeof(SyntaxTreeVisitor), node.GetType());

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
                // TODO: Parse Namespaces.
                // TODO: Reorder Namespaces with Crosslight rules using Identifiers.
                var namespaces = node.Children.OfType<NamespaceDeclaration>();

                var others = node.Children
                    //.Except(usings)
                    //.Except(assemblyAttributes)
                    //.Except(namespaces)
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
    }
}
