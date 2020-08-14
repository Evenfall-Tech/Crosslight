using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// FunctionReturnTypeNode represents the return type of the function.
    /// </summary>
    public class FunctionReturnTypeNode : Node
    {
        public FunctionReturnTypeNode()
        {
        }
        public override string ToString()
        {
            return "FunctionReturnTypeNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
