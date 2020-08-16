using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// LoopNode represents the loop.
    /// </summary>
    public class LoopNode : StatementNode
    {
        public LoopNode()
        {
        }
        public override string ToString()
        {
            return "LoopNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
