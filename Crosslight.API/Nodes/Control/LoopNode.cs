using Crosslight.Common.Util;
using System;

namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// LoopNode represents the loop.
    /// </summary>
    public class LoopNode : StatementNode
    {
        public override Type Type => typeof(LoopNode);
        private readonly SyncedProperty<BlockNode, Node> body;
        private readonly SyncedProperty<ExpressionNode, Node> condition;
        public BlockNode Body
        {
            get => body.Value;
            set => body.Value = value;
        }
        public ExpressionNode Condition
        {
            get => condition.Value;
            set => condition.Value = value;
        }
        public LoopNode()
        {
            body = new SyncedProperty<BlockNode, Node>(Children);
            condition = new SyncedProperty<ExpressionNode, Node>(Children);
        }
        public override string ToString()
        {
            return "LoopNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
