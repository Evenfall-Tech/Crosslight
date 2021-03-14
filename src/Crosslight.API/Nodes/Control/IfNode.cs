using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// <see cref="IfNode"/> represents the if-else statement.
    /// </summary>
    public class IfNode : StatementNode
    {
        public override string Type => nameof(IfNode);
        private readonly SyncedProperty<ExpressionNode, Node> condition;
        private readonly SyncedProperty<BlockNode, Node> ifBlock;
        private readonly SyncedProperty<BlockNode, Node> elseBlock;
        public SyncedList<ExpressionNode, Node> ElseIfConditions { get; protected set; }
        public SyncedList<BlockNode, Node> ElseIfBlocks { get; protected set; }
        public ExpressionNode Condition
        {
            get => condition.Value;
            protected set => condition.Value = value;
        }
        public BlockNode IfBlock
        {
            get => ifBlock.Value;
            protected set => ifBlock.Value = value;
        }
        public BlockNode ElseBlock
        {
            get => elseBlock.Value;
            protected set => elseBlock.Value = value;
        }
        public IfNode()
        {
            condition = new SyncedProperty<ExpressionNode, Node>(Children);
            ifBlock = new SyncedProperty<BlockNode, Node>(Children);
            elseBlock = new SyncedProperty<BlockNode, Node>(Children);
            ElseIfConditions = new SyncedList<ExpressionNode, Node>(Children);
            ElseIfBlocks = new SyncedList<BlockNode, Node>(Children);
        }
        public override string ToString()
        {
            return "IfNode";
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
