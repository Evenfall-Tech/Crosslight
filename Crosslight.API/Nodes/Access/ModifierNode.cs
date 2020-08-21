using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// ModifierNode represents the modifier (public, abstract, virtual).
    /// </summary>
    public class ModifierNode : Node
    {
        public override Type Type => typeof(ModifierNode);
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
