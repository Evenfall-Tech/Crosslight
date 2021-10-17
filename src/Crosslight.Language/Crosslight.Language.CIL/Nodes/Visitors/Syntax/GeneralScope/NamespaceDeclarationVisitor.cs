using Crosslight.API.Exceptions;
using Crosslight.API.Nodes;
using Crosslight.API.Nodes.Componentization;
using Crosslight.API.Nodes.Entities;
using ICSharpCode.Decompiler.CSharp.Syntax;
using System;
using System.Linq;

namespace Crosslight.Language.CIL.Nodes.Visitors.Syntax.GeneralScope
{
    public class NamespaceDeclarationVisitor : AbstractVisitor<NamespaceDeclaration>
    {
        public NamespaceDeclarationVisitor(VisitContext context) : base(context)
        {

        }

        public override Node Visit(AstNode node)
        {
            if (node == null) throw new NullReferenceException("Passed node was null.");

            try
            {
                if (!(node is NamespaceDeclaration namespaceDeclaration))
                    throw new WrongVisitorException(nameof(NamespaceDeclarationVisitor), node.GetType().Name);

                return Visit(namespaceDeclaration);
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

        public override Node Visit(NamespaceDeclaration node)
        {
            try
            {
                var root = new NamespaceNode(node.Identifiers);
                foreach (var m in node.Members)
                {
                    if (m is TypeDeclaration td)
                    {
                        var visitor = Context?.VisitFactory?.GetVisitor(nameof(TypeDeclaration)) as ICILVisitor<TypeDeclaration>;
                        Node outNode = td.AcceptVisitor(visitor);
                        if (!(outNode is EntityNode entity))
                        {
                            throw new VisitorException($"{nameof(TypeDeclaration)} visitor returned {outNode.Type}.");
                        }
                        root.Entities.Add(entity);
                    }
                    else if (m is DelegateDeclaration dd)
                    {
                        var visitor = Context?.VisitFactory?.GetVisitor(nameof(DelegateDeclaration)) as ICILVisitor<DelegateDeclaration>;
                        Node outNode = dd.AcceptVisitor(visitor);
                        if (!(outNode is FunctionEntityNode delegateNode))
                        {
                            throw new VisitorException($"{nameof(DelegateDeclaration)} visitor returned {outNode.Type}.");
                        }
                        root.Entities.Add(delegateNode);
                    }
                    else
                    {
                        throw new NotImplementedException($"{m.GetType()} is not supported in namespaces.");
                    }
                }
                foreach (var c in node.Children.Except(node.Members).Except(new AstNode[] { node.NamespaceName }))
                {
                    Node outNode = Context?.VisitFactory?.GetVisitor(c)?.Visit(c);
                    if (outNode != null)
                    {
                        root.Children.Add(outNode);
                    }
                }
                return root;
            }
            catch (Exception e)
            {
                throw new VisitorException(e);
            }
        }

        public override Node VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
        {
            return Visit(namespaceDeclaration);
        }
    }
}
