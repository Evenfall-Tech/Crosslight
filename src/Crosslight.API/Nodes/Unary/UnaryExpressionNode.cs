using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Unary
{
    /// <summary>
    /// <see cref="UnaryExpressionNode"/> represents the unary expression.
    /// </summary>
    public abstract class UnaryExpressionNode : ExpressionNode
    {
        public override string Type => nameof(UnaryExpressionNode);
        private readonly SyncedProperty<ExpressionNode, Node> operand;
        public ExpressionNode Operand
        {
            get => operand.Value;
            protected set => operand.Value = value;
        }
        public UnaryExpressionNode()
        {
            operand = new SyncedProperty<ExpressionNode, Node>(Children);
        }
        public override string ToString()
        {
            return "UnaryExpressionNode";
        }
    }
}
