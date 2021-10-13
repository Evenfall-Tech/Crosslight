using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// <see cref="SwitchNode"/> represents the switch operator.
    /// </summary>
    public class SwitchNode : StatementNode
    {
        public override string Type => nameof(SwitchNode);
        private readonly SyncedProperty<ExpressionNode, Node> conditionExpression;
        private readonly SyncedProperty<BlockNode, Node> defaultBlock;
        public SyncedList<ExpressionNode, Node> Cases { get; protected set; }
        public SyncedList<BlockNode, Node> Blocks { get; protected set; }
        public ExpressionNode ConditionExpression
        {
            get => conditionExpression.Value;
            protected set => conditionExpression.Value = value;
        }
        public BlockNode DefaultBlock
        {
            get => defaultBlock.Value;
            protected set => defaultBlock.Value = value;
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
        // TODO: fix this.
        /*public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<S>(IVisitor<S> visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<T, S>(IVisitor<T, S> visitor, T data)
        {
            return visitor.Visit(this, data);
        }*/
    }
}
