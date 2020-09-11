using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Function;
using Crosslight.API.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crosslight.API.Nodes.Componentization
{
    /// <summary>
    /// NamespaceNode represents the namespace abstraction in the language.
    /// </summary>
    public class NamespaceNode : Node
    {
        public override Type Type => typeof(NamespaceNode);
        public SyncedList<TypeNode, Node> Types { get; protected set; }
        public SyncedList<ValueNode, Node> Values { get; protected set; }
        public SyncedList<FunctionNode, Node> Functions { get; protected set; }
        public SyncedList<NamespaceNode, Node> Namespaces { get; protected set; }
        public string[] Identifiers { get; }
        public string FullName { get => throw new NotImplementedException(); }
        public NamespaceNode(string name) : this(name.Split('.'))
        { }
        public NamespaceNode(IEnumerable<string> identifiers)
        {
            Namespaces = new SyncedList<NamespaceNode, Node>(Children);
            Types = new SyncedList<TypeNode, Node>(Children);
            Functions = new SyncedList<FunctionNode, Node>(Children);
            Values = new SyncedList<ValueNode, Node>(Children);
            Identifiers = identifiers.ToArray();
        }
        public override string ToString()
        {
            return $"Namespace {string.Join(".", Identifiers)}";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
