using Crosslight.API.Nodes.Access.Modifiers;
using Crosslight.API.Nodes.Entities;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;
using System;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// <see cref="FieldNode"/> represents the field abstraction in the language.
    /// </summary>
    public class FieldNode : VariableNode, ITypeMemberNode
    {
        public override string Type => nameof(FieldNode);
        public FunctionalTypeNode OwningType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SyncedList<AttributeNode, Node> Attributes { get; protected set; }
        public SyncedList<ModifierNode, Node> Modifiers { get; protected set; }
        public FieldNode(string name) : base(name)
        {
            Attributes = new SyncedList<AttributeNode, Node>(Children);
            Modifiers = new SyncedList<ModifierNode, Node>(Children);
        }
        public override string ToString()
        {
            return "FieldNode";
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
    }
}
