using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// ContinueNode represents the continue statement.
    /// </summary>
    public class ContinueNode: JumpNode
    {
        public override Type Type => typeof(ContinueNode);
        public ContinueNode()
        {
        }
        public override string ToString()
        {
            return "ContinueNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
