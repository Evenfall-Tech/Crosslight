using Crosslight.API.Nodes.Implementations;
using ICSharpCode.Decompiler.CSharp.Syntax;

namespace Crosslight.Transformer.CIL.Nodes.Visitors
{
    public interface ICILVisitor
    {
        bool CanBeVisited(AstNode node);
        Node Visit(AstNode node);
    }

    public interface ICILVisitor<T> : IAstVisitor<Node>, ICILVisitor where T : AstNode
    {
        Node Visit(T node);
    }
}
