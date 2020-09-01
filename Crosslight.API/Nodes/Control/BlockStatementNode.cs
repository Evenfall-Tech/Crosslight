using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// BlockStatementNode represents something.
    /// </summary>
    public class BlockStatementNode : StatementNode
    {
        public override Type Type => typeof(BlockStatementNode);
        public BlockStatementNode()
        {
        }
        public override string ToString()
        {
            return "BlockStatementNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
