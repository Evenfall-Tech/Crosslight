using Crosslight.API.Nodes;
using ICSharpCode.Decompiler.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.CIL.Nodes.Visitors
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
