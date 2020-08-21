using Crosslight.API.Util;
using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    /// <summary>
    /// ModuleNode represents the module abstraction in the language.
    /// E.g. in C# it is module, for Java it is package,
    /// in C++ it is library.
    /// </summary>
    public class ModuleNode : Node
    {
        public override Type Type => typeof(ModuleNode);
        public SyncedList<NamespaceNode, Node> Namespaces { get; protected set; }
        public ModuleNode()
        {
            Namespaces = new SyncedList<NamespaceNode, Node>(Children);
        }
        public override string ToString()
        {
            return "ModuleNode";
        }
        public override object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
