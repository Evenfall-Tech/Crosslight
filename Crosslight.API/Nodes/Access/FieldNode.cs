using Crosslight.API.Util;
using System;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// FieldNode represents the field abstraction in the language.
    /// </summary>
    public class FieldNode : ValueNode, ITypeMember
    {
        public override Type Type => typeof(FieldNode);
        public TypeNode parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SyncedList<AttributeNode, Node> Attributes { get; protected set; }
        public SyncedList<ModifierNode, Node> Modifiers { get; protected set; }
        public string Name { get; }
        public FieldNode(string name)
        {
            Attributes = new SyncedList<AttributeNode, Node>(Children);
            Modifiers = new SyncedList<ModifierNode, Node>(Children);
            Name = name;
        }
        public override string ToString()
        {
            return "FieldNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
