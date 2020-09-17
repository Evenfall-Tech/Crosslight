using System;

namespace Crosslight.API.Nodes.Control
{
    /// <summary>
    /// ContinueNode represents the continue statement.
    /// </summary>
    public class ContinueNode : JumpNode
    {
        public override string Type => nameof(ContinueNode);
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
