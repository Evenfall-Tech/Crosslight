using Crosslight.API.Nodes;
using Crosslight.API.Nodes.Componentization;
using Crosslight.API.Nodes.Entities;

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

        public object Visit(FunctionEntityNode node)
        {
            return Visit((Node)node);
        }

        public object Visit(EnumNode node)
        {
            return Visit((Node)node);
        }

        public object Visit(StructNode node)
        {
            return Visit((Node)node);
        }

        public object Visit(InterfaceNode node)
        {
            return Visit((Node)node);
        }

        public object Visit(ClassNode node)
        {
            return Visit((Node)node);
        }

        public object Visit(NamespaceNode node)
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
    }
}
