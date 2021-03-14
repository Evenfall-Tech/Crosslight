using Crosslight.API.Exceptions;
using Crosslight.API.Nodes;
using Crosslight.API.Nodes.Entities;
using ICSharpCode.Decompiler.CSharp.Syntax;
using System;

namespace Crosslight.Language.CIL.Nodes.Visitors.Syntax.GeneralScope
{
    public class TypeDeclarationVisitor : AbstractVisitor<TypeDeclaration>
    {
        public TypeDeclarationVisitor(VisitContext context) : base(context)
        {

        }

        public override Node Visit(AstNode node)
        {
            if (node == null) throw new NullReferenceException("Passed node was null.");

            try
            {
                if (!(node is TypeDeclaration typeDeclaration))
                    throw new WrongVisitorException(nameof(TypeDeclarationVisitor), node.GetType().Name);

                return Visit(typeDeclaration);
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

        public override Node Visit(TypeDeclaration node)
        {
            try
            {
                EntityNode root;
                switch (node.ClassType)
                {
                    case ClassType.Class:
                        root = VisitClass(node);
                        break;
                    case ClassType.Struct:
                        root = VisitStruct(node);
                        break;
                    case ClassType.Interface:
                        root = VisitInterface(node);
                        break;
                    case ClassType.Enum:
                        root = VisitEnum(node);
                        break;
                    default:
                        throw new ArgumentException($"{node.ClassType} is not supported in {nameof(TypeDeclaration)}.");
                }
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

        private ClassNode VisitClass(TypeDeclaration node)
        {
            return new ClassNode(node.Name);
        }

        private StructNode VisitStruct(TypeDeclaration node)
        {
            return new StructNode(node.Name);
        }

        private InterfaceNode VisitInterface(TypeDeclaration node)
        {
            return new InterfaceNode(node.Name);
        }

        private EnumNode VisitEnum(TypeDeclaration node)
        {
            return new EnumNode(node.Name);
        }

        public override Node VisitTypeDeclaration(TypeDeclaration typeDeclaration)
        {
            return Visit(typeDeclaration);
        }
    }
}
