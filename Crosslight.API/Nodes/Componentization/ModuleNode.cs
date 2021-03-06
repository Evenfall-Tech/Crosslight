﻿using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Metadata;
using Crosslight.API.Util;
using System;

namespace Crosslight.API.Nodes.Componentization
{
    /// <summary>
    /// <see cref="ModuleNode"/> represents the module abstraction in the language.
    /// E.g. in C# it is module, for Java it is package,
    /// in C++ it is library.
    /// </summary>
    public class ModuleNode : Node
    {
        public override string Type => nameof(ModuleNode);
        public SyncedList<AttributeNode, Node> Attributes { get; protected set; }
        public SyncedList<NamespaceNode, Node> Namespaces { get; protected set; }
        public string ModuleName { get; }
        public ModuleNode(string name)
        {
            Attributes = new SyncedList<AttributeNode, Node>(Children);
            Namespaces = new SyncedList<NamespaceNode, Node>(Children);
            ModuleName = name;
        }
        public override string ToString()
        {
            return $"Module {ModuleName}";
        }
        public override object AcceptVisitor(IVisitor visitor)
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
        }
    }
}
