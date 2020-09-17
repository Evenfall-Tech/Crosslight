using System;

namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// BlockStatementNode represents something.
    /// </summary>
    public class BlockStatementNode : StatementNode
    {
        public override string Type => nameof(BlockStatementNode);
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
