using Crosslight.API.Exceptions;
using Crosslight.API.Nodes;
using Crosslight.API.Nodes.Componentization;
using ICSharpCode.Decompiler.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.CIL.Nodes.Visitors.Syntax.GeneralScope
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
                foreach (var c in node.Children)
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
