using Crosslight.API.Util;
using System;

namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// BlockNode represents the block of code.
    /// </summary>
    public class BlockNode : Node
    {
        public override string Type => nameof(BlockNode);
        public SyncedList<StatementNode, Node> Statements { get; protected set; }
        // scope
        public BlockNode()
        {
            Statements = new SyncedList<StatementNode, Node>(Children);
        }
        public override string ToString()
        {
            return "BlockNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
