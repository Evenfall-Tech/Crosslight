using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Binary
{
    /// <summary>
    /// <see cref="BinaryExpressionNode"/> represents the binary expression.
    /// </summary>
    public abstract class BinaryExpressionNode : ExpressionNode
    {
        public override string Type => nameof(BinaryExpressionNode);
        private readonly SyncedProperty<ExpressionNode, Node> leftOperand;
        private readonly SyncedProperty<ExpressionNode, Node> rightOperand;
        public ExpressionNode LeftOperand
        {
            get => leftOperand.Value;
            protected set => leftOperand.Value = value;
        }
        public ExpressionNode RightOperand
        {
            get => rightOperand.Value;
            protected set => rightOperand.Value = value;
        }
        public BinaryExpressionNode()
        {
            leftOperand = new SyncedProperty<ExpressionNode, Node>(Children);
            rightOperand = new SyncedProperty<ExpressionNode, Node>(Children);
        }
        public override string ToString()
        {
            return "BinaryExpressionNode";
        }
    }
}
