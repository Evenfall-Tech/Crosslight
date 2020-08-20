using Crosslight.API.Util;
using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// SwitchNode represents the switch operator.
    /// </summary>
    public class SwitchNode : StatementNode
    {
        public override Type Type => typeof(SwitchNode);
        private readonly SyncedProperty<ExpressionNode, Node> conditionExpression;
        private readonly SyncedProperty<BlockNode, Node> defaultBlock;
        public SyncedList<ExpressionNode, Node> Cases { get; protected set; }
        public SyncedList<BlockNode, Node> Blocks { get; protected set; }
        public ExpressionNode ConditionExpression
        {
            get => conditionExpression.Value;
            set => conditionExpression.Value = value;
        }
        public BlockNode DefaultBlock
        {
            get => defaultBlock.Value;
            set => defaultBlock.Value = value;
        }
        public SwitchNode()
        {
            conditionExpression = new SyncedProperty<ExpressionNode, Node>(Children);
            Cases = new SyncedList<ExpressionNode, Node>(Children);
            Blocks = new SyncedList<BlockNode, Node>(Children);
            defaultBlock = new SyncedProperty<BlockNode, Node>(Children);
        }
        public override string ToString()
        {
            return "SwitchNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
