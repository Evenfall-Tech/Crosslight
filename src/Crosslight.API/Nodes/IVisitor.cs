using Crosslight.API.Nodes.Implementations.Componentization;
using Crosslight.API.Nodes.Implementations.Entities;

namespace Crosslight.API.Nodes
{
    public interface IVisitor
    {
        // object Visit(Node node);
        object Visit(FunctionEntityDeclarationNode node);
        object Visit(EnumDeclarationNode node);
        object Visit(StructDeclarationNode node);
        object Visit(InterfaceDeclarationNode node);
        object Visit(ClassDeclarationNode node);
        object Visit(CustomCompoundTypeDeclarationNode node);
        object Visit(NamespaceDeclarationNode node);
        object Visit(ModuleNode node);
        object Visit(ProjectNode node);
    }
    public interface IVisitor<out S>
    {
        // S Visit(Node node);
        S Visit(FunctionEntityDeclarationNode node);
        S Visit(EnumDeclarationNode node);
        S Visit(StructDeclarationNode node);
        S Visit(InterfaceDeclarationNode node);
        S Visit(ClassDeclarationNode node);
        S Visit(CustomCompoundTypeDeclarationNode node);
        S Visit(NamespaceDeclarationNode node);
        S Visit(ModuleNode node);
        S Visit(ProjectNode node);
    }
    public interface IVisitor<in T, out S>
    {
        // S Visit(Node node, T data);
        S Visit(FunctionEntityDeclarationNode node, T data);
        S Visit(EnumDeclarationNode node, T data);
        S Visit(StructDeclarationNode node, T data);
        S Visit(InterfaceDeclarationNode node, T data);
        S Visit(ClassDeclarationNode node, T data);
        S Visit(CustomCompoundTypeDeclarationNode node, T data);
        S Visit(NamespaceDeclarationNode node, T data);
        S Visit(ModuleNode node, T data);
        S Visit(ProjectNode node, T data);
    }
}
