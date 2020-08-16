using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// BinaryExpressionNode represents the binary expression.
    /// </summary>
    public class BinaryExpressionNode : ExpressionNode
    {
        public BinaryExpressionNode()
        {
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
