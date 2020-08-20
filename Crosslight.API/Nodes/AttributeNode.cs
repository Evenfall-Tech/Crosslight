using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// AttributeNode represents the attribute/annotation abstraction in the language.
    /// </summary>
    public class AttributeNode : Node
    {
        public override Type Type => typeof(AttributeNode);
        public AttributeNode()
        {
        }
        public override string ToString()
        {
            return "AttributeNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
