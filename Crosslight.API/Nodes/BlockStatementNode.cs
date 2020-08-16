using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// BlockStatementNode represents something.
    /// </summary>
    public class BlockStatementNode : StatementNode
    {
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
