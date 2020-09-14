using System;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// AttributeNode represents the attribute/annotation abstraction in the language.
    /// </summary>
    public class AttributeNode : Node
    {
        // TODO: add attribute types to attribute node
        public override string Type => nameof(AttributeNode);
        public string Name { get; }
        public AttributeNode(string name)
        {
            Name = name;
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
