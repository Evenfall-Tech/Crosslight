using System;

namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// BreakNode represents the break statement.
    /// </summary>
    public class BreakNode : JumpNode
    {
        public override string Type => nameof(BreakNode);
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
