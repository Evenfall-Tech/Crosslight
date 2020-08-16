using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// UnaryExpressionNode represents the unary expression.
    /// </summary>
    public class UnaryExpressionNode : ExpressionNode
    {
        public UnaryExpressionNode()
        {
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
