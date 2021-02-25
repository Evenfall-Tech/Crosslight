using Crosslight.API.Nodes.Componentization;
using Crosslight.API.Nodes.Entities;

namespace Crosslight.API.Nodes
{
    public interface IVisitor
    {
        // object Visit(Node node);
        object Visit(FunctionEntityNode node);
        object Visit(EnumNode node);
        object Visit(StructNode node);
        object Visit(InterfaceNode node);
        object Visit(ClassNode node);
        object Visit(NamespaceNode node);
        object Visit(ModuleNode node);
        object Visit(ProjectNode node);
    }
    public interface IVisitor<out S>
    {
        // S Visit(Node node);
        S Visit(FunctionEntityNode node);
        S Visit(EnumNode node);
        S Visit(StructNode node);
        S Visit(InterfaceNode node);
        S Visit(ClassNode node);
        S Visit(NamespaceNode node);
        S Visit(ModuleNode node);
        S Visit(ProjectNode node);
    }
    public interface IVisitor<in T, out S>
    {
        // S Visit(Node node, T data);
        S Visit(FunctionEntityNode node, T data);
        S Visit(EnumNode node, T data);
        S Visit(StructNode node, T data);
        S Visit(InterfaceNode node, T data);
        S Visit(ClassNode node, T data);
        S Visit(NamespaceNode node, T data);
        S Visit(ModuleNode node, T data);
        S Visit(ProjectNode node, T data);
    }
}
