using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Function;
using Crosslight.Common.Util;
using System;

namespace Crosslight.API.Nodes
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
        public string Name { get; }
        public NamespaceNode(string name)
        {
            Types = new SyncedList<TypeNode, Node>(Children);
            Values = new SyncedList<ValueNode, Node>(Children);
            Functions = new SyncedList<FunctionNode, Node>(Children);
            Name = name;
        }
        public override string ToString()
        {
            return "NamespaceNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
