using Crosslight.API.Nodes.Function;
using Crosslight.API.Util;
using System;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// TypeNode represents the type abstraction in the language.
    /// E.g. in C# it is class/struct, for Java it is class,
    /// in C++ it is class/struct.
    /// </summary>
    public abstract class TypeNode : Node
    {
        public override Type Type => typeof(TypeNode);
        public SyncedList<FieldNode, Node> Fields { get; protected set; }
        public SyncedList<MethodNode, Node> Methods { get; protected set; }
        public SyncedList<AttributeNode, Node> Attributes { get; protected set; }
        public SyncedList<ModifierNode, Node> Modifiers { get; protected set; }
        public string Name { get; }
        public TypeNode(string name)
        {
            Fields = new SyncedList<FieldNode, Node>(Children);
            Methods = new SyncedList<MethodNode, Node>(Children);
            Attributes = new SyncedList<AttributeNode, Node>(Children);
            Modifiers = new SyncedList<ModifierNode, Node>(Children);
            Name = name;
        }
        public override string ToString()
        {
            return "TypeNode";
        }
    }
}
