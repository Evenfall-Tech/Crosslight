using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// BreakNode represents the break statement.
    /// </summary>
    public class BreakNode : JumpNode
    {
        public override Type Type => typeof(BreakNode);
        public BreakNode()
        {
        }
        public override string ToString()
        {
            return "BreakNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
