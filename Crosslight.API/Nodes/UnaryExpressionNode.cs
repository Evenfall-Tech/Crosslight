using Crosslight.API.Util;
using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// UnaryExpressionNode represents the unary expression.
    /// </summary>
    public abstract class UnaryExpressionNode : ExpressionNode
    {
        public override Type Type => typeof(UnaryExpressionNode);
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
