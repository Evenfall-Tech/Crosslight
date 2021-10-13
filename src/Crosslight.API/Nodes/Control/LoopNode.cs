using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// <see cref="LoopNode"/> represents the loop.
    /// </summary>
    public class LoopNode : StatementNode
    {
        public override string Type => nameof(LoopNode);
        private readonly SyncedProperty<BlockNode, Node> body;
        private readonly SyncedProperty<ExpressionNode, Node> condition;
        public BlockNode Body
        {
            get => body.Value;
            protected set => body.Value = value;
        }
        public ExpressionNode Condition
        {
            get => condition.Value;
            protected set => condition.Value = value;
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
