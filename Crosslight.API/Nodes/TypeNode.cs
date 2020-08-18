using Crosslight.API.Util;
using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// TypeNode represents the type abstraction in the language.
    /// E.g. in C# it is class/struct, for Java it is class,
    /// in C++ it is class/struct.
    /// </summary>
    public abstract class TypeNode : Node
    {
        public SyncedList<FieldNode, Node> Fields { get; protected set; }
        public SyncedList<MethodNode, Node> Methods { get; protected set; }
        public SyncedList<AttributeNode, Node> Attributes { get; protected set; }
        public SyncedList<ModifierNode, Node> Modifiers { get; protected set; }
        public TypeNode()
        {
            Fields = new SyncedList<FieldNode, Node>(Children);
            Methods = new SyncedList<MethodNode, Node>(Children);
            Attributes = new SyncedList<AttributeNode, Node>(Children);
            Modifiers = new SyncedList<ModifierNode, Node>(Children);
        }
        public override string ToString()
        {
            return "TypeNode";
        }
    }
}
