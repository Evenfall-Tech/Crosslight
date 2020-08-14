using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// FunctionParameterNode represents parameters of the function.
    /// </summary>
    public class FunctionParameterNode : Node
    {
        public FunctionParameterNode()
        {
        }
        public override string ToString()
        {
            return "FunctionParameterNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
