using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// StructNode represents the struct abstraction in the language.
    /// </summary>
    public class StructNode : TypeNode
    {
        public override Type Type => typeof(StructNode);
        public StructNode()
        {
        }
        public override string ToString()
        {
            return "StructNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
