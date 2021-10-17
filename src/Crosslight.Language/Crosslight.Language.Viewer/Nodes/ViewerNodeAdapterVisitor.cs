using Crosslight.API.Nodes;
using Crosslight.API.Nodes.Implementations;
using Crosslight.API.Nodes.Implementations.Componentization;
using Crosslight.API.Nodes.Implementations.Entities;

namespace Crosslight.Language.Viewer.Nodes
{
    public class ViewerNodeAdapterVisitor : IVisitor
    {
        public object Visit(Node node)
        {
            var result = new ViewerNode(node);
            foreach (var child in node.Children)
            {
                result.Children.Add((Node)child.AcceptVisitor(this));
            }
            return result;
        }

        public object Visit(FunctionEntityDeclarationNode node)
        {
            return Visit((Node)node);
        }

        public object Visit(EnumDeclarationNode node)
        {
            return Visit((Node)node);
        }

        public object Visit(StructDeclarationNode node)
        {
            return Visit((Node)node);
        }

        public object Visit(InterfaceDeclarationNode node)
        {
            return Visit((Node)node);
        }

        public object Visit(ClassDeclarationNode node)
        {
            return Visit((Node)node);
        }

        public object Visit(NamespaceDeclarationNode node)
        {
            return Visit((Node)node);
        }

        public object Visit(ModuleNode node)
        {
            return Visit((Node)node);
        }

        public object Visit(ProjectNode node)
        {
            return Visit((Node)node);
        }

        public object Visit(CustomCompoundTypeDeclarationNode node)
        {
            return Visit((Node)node);
        }
    }
}
