using System;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// <see cref="AttributeNode"/> represents the attribute/annotation abstraction in the language.
    /// </summary>
    public class AttributeNode : Node, IEquatable<AttributeNode>
    {
        // TODO: add attribute types, constructor and parameters to AttributeNode.
        public override string Type => nameof(AttributeNode);
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
        // TODO: fix this.
        /*public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<S>(IVisitor<S> visitor)
        {
            return visitor.Visit(this);
        }
        public override S AcceptVisitor<T, S>(IVisitor<T, S> visitor, T data)
        {
            return visitor.Visit(this, data);
        }*/

        public bool Equals(AttributeNode other)
        {
            return Name == other.Name && Options == other.Options;
            // TODO: update this method once AttributeNode has types, constructor and parameters.
        }
    }
}
