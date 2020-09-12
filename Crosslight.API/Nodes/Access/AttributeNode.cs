using System;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// AttributeNode represents the attribute/annotation abstraction in the language.
    /// </summary>
    public class AttributeNode : Node
    {
        // TODO: add attribute types, constructor and parameters to AttributeNode.
        public override Type Type => typeof(AttributeNode);
        public AttributeOptions Options { get; set; }
        public string Name { get; }
        public AttributeNode(string name)
        {
            Name = name;
            Options = new AttributeOptions();
        }
        public override string ToString()
        {
            return $"Attribute {Name} {Options}";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
