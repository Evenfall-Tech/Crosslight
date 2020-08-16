using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// ExpressionValueNode represents the value of the expression.
    /// </summary>
    public class ExpressionValueNode : ExpressionNode
    {
        ValueNode Value { get; set; }
        public ExpressionValueNode()
        {
        }
        public override string ToString()
        {
            return "ExpressionValueNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
