using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// FunctionNode represents the function abstraction in the language.
    /// </summary>
    public class FunctionNode : Node
    {
        public FunctionNode()
        {
        }
        public override string ToString()
        {
            return "FunctionNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
