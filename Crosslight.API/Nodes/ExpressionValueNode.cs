using Crosslight.API.Nodes.Access;
using System;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// ExpressionValueNode represents the value of the expression.
    /// </summary>
    public class ExpressionValueNode : ExpressionNode
    {
        public override string Type => nameof(ExpressionValueNode);
        public ValueNode Value { get; set; }
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
        public override S AcceptVisitor<S>(IVisitor<S> visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<T, S>(IVisitor<T, S> visitor, T data)
        {
            return visitor.Visit(this, data);
        }
    }
}
