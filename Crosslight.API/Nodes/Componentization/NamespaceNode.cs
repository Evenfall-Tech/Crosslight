using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Entities;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;
using System.Collections.Generic;
using System.Linq;

namespace Crosslight.API.Nodes.Componentization
{
    /// <summary>
    /// <see cref="NamespaceNode"/> represents the namespace abstraction in the language.
    /// </summary>
    public class NamespaceNode : Node, INamedNode, IAttributedNode
    {
        public override string Type => nameof(NamespaceNode);
        public SyncedList<AttributeNode, Node> Attributes { get; protected set; }
        public SyncedList<EntityNode, Node> Entities { get; protected set; }
        public SyncedList<ValueNode, Node> Values { get; protected set; }
        public SyncedList<NamespaceNode, Node> Namespaces { get; protected set; }
        public string[] Identifiers { get; }
        public string Name => string.Join(".", Identifiers);
        public NamespaceNode(string name) : this(name.Split('.'))
        { }
        public NamespaceNode(IEnumerable<string> identifiers)
        {
            Attributes = new SyncedList<AttributeNode, Node>(Children);
            Namespaces = new SyncedList<NamespaceNode, Node>(Children);
            Entities = new SyncedList<EntityNode, Node>(Children);
            Values = new SyncedList<ValueNode, Node>(Children);
            Identifiers = identifiers.ToArray();
        }
        public override string ToString()
        {
            return $"Namespace {string.Join(".", Identifiers)}";
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
