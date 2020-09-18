using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// EnumNode represents the enum abstraction in the language.
    /// </summary>
    public class EnumNode : TypeNode
    {
        public override string Type => nameof(EnumNode);
        public EnumNode(string name) : base(name)
        {
        }
        public override string ToString()
        {
            return "EnumNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
