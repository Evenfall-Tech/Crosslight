using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// FunctionBodyNode represents the body of the function.
    /// </summary>
    public class FunctionBodyNode : Node
    {
        public FunctionBodyNode()
        {
        }
        public override string ToString()
        {
            return "FunctionBodyNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
