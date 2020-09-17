using System;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// StructNode represents the struct abstraction in the language.
    /// </summary>
    public class StructNode : TypeNode
    {
        public override string Type => nameof(StructNode);
        public StructNode(string name) : base(name)
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
