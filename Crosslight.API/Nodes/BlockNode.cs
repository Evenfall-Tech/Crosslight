using Crosslight.API.Util;
using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// BlockNode represents the block of code.
    /// </summary>
    public class BlockNode : Node
    {
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
