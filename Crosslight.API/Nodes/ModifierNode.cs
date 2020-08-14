using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// ModifierNode represents the modifier (public, abstract, virtual).
    /// </summary>
    public class ModifierNode : Node
    {
        public ModifierNode()
        {
        }
        public override string ToString()
        {
            return "ModifierNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
