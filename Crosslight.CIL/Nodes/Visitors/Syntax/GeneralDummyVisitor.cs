using Crosslight.API.Nodes;
using ICSharpCode.Decompiler.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crosslight.CIL.Nodes.Visitors.Syntax
{
    public class GeneralDummyVisitor : ICILVisitor
    {
        private readonly DummyCILAstVisitor visitor;
        public GeneralDummyVisitor()
        {
            visitor = new DummyCILAstVisitor();
        }

        public bool CanBeVisited(AstNode node) => true;

        public Node Visit(AstNode node)
        {
            return node.AcceptVisitor(visitor);
        }
    }
}
