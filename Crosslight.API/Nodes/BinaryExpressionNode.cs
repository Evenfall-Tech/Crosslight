using Crosslight.API.Util;
using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// BinaryExpressionNode represents the binary expression.
    /// </summary>
    public abstract class BinaryExpressionNode : ExpressionNode
    {
        public override Type Type => typeof(BinaryExpressionNode);
        private readonly SyncedProperty<ExpressionNode, Node> leftOperand;
        private readonly SyncedProperty<ExpressionNode, Node> rightOperand;
        public ExpressionNode LeftOperand
        {
            get => leftOperand.Value;
            set => leftOperand.Value = value;
        }
        public ExpressionNode RightOperand
        {
            get => rightOperand.Value;
            set => rightOperand.Value = value;
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
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
