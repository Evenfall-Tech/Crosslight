﻿using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Nodes.Implementations.Expressions;

namespace Crosslight.API.Nodes.Implementations
{
    /// <summary>
    /// <see cref="ExpressionValueNode"/> represents the value of the expression.
    /// </summary>
    public class ExpressionValueNode : ExpressionNode
    {
        public override string Type => nameof(ExpressionValueNode);
        // TODO: fix this
        //public ValueNode Value { get; set; }
        public ExpressionValueNode()
        {
        }
        public override string ToString()
        {
            return Type;
        }
        // TODO: fix this.
        /*public override object AcceptVisitor(IVisitor visitor)
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
        }*/
    }
}