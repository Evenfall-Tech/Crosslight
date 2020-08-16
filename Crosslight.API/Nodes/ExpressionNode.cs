using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// ExpressionNode represents the expression.
    /// </summary>
    public abstract class ExpressionNode : Node
    {
        public ExpressionNode()
        {
        }
        public override string ToString()
        {
            return "ExpressionNode";
        }
    }
}
