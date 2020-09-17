using Crosslight.API.Util;
using System;

namespace Crosslight.API.Nodes.Unary
{
    /// <summary>
    /// UnaryExpressionNode represents the unary expression.
    /// </summary>
    public abstract class UnaryExpressionNode : ExpressionNode
    {
        public override string Type => nameof(UnaryExpressionNode);
        private readonly SyncedProperty<ExpressionNode, Node> operand;
        public ExpressionNode Operand
        {
            get => operand.Value;
            set => operand.Value = value;
        }
        public UnaryExpressionNode()
        {
            operand = new SyncedProperty<ExpressionNode, Node>(Children);
        }
        public override string ToString()
        {
            return "UnaryExpressionNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
