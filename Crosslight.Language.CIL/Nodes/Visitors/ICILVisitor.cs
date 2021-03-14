using Crosslight.API.Nodes;
using ICSharpCode.Decompiler.CSharp.Syntax;

namespace Crosslight.Language.CIL.Nodes.Visitors
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
