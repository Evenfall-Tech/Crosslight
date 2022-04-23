using Crosslight.API.Exceptions;
using Crosslight.API.Nodes.Implementations;
using Crosslight.API.Nodes.Implementations.Entities;
using ICSharpCode.Decompiler.CSharp.Syntax;
using System;

namespace Crosslight.Transformer.CIL.Nodes.Visitors.Syntax.GeneralScope
{
    public class DelegateDeclarationVisitor : AbstractVisitor<DelegateDeclaration>
    {
        public DelegateDeclarationVisitor(VisitContext context) : base(context)
        {

        }

        public override Node Visit(AstNode node)
        {
            if (node == null) throw new NullReferenceException("Passed node was null.");

            try
            {
                if (!(node is DelegateDeclaration delegateDeclaration))
                    throw new WrongVisitorException(nameof(DelegateDeclarationVisitor), node.GetType().Name);

                return Visit(delegateDeclaration);
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

        public override Node Visit(DelegateDeclaration node)
        {
            try
            {
                FunctionEntityDeclarationNode root = new FunctionEntityDeclarationNode(null);
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

        public override Node VisitDelegateDeclaration(DelegateDeclaration delegateDeclaration)
        {
            return Visit(delegateDeclaration);
        }
    }
}
