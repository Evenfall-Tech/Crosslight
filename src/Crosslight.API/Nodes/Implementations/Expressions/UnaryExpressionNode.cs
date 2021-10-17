using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Expressions
{
    /// <summary>
    /// <see cref="UnaryExpressionNode"/> represents the unary expression.
    /// </summary>
    public class UnaryExpressionNode : ExpressionNode
    {
        public override string Type => nameof(UnaryExpressionNode);

        public bool IsPrefix { get => !unaryKind; set => unaryKind = false; }
        public bool IsPostfix { get => unaryKind; set => unaryKind = true; }

        // TODO: list all possible types
        // TODO: define constants for ExpressionType
        public string ExpressionType { get; set; }

        private readonly SyncedProperty<ExpressionNode, Node> operand;

        /// <summary>
        /// false is prefix, true is postfix
        /// </summary>
        private bool unaryKind;

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
            return nameof(UnaryExpressionNode);
        }
    }
}
