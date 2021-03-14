using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Entities;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Componentization
{
    /// <summary>
    /// <see cref="ModuleNode"/> represents the module abstraction in the language.
    /// E.g. in C# it is module, for Java it is package,
    /// in C++ it is library.
    /// </summary>
    public class ModuleNode : AttributedNode, INamedNode, IAttributedNode
    {
        public override string Type => nameof(ModuleNode);
        public SyncedList<NamespaceNode, Node> Namespaces { get; protected set; }
        public string Name { get; }
        public ModuleNode(string name)
        {
            Namespaces = new SyncedList<NamespaceNode, Node>(Children);
            Name = name;
        }
        public override string ToString()
        {
            return $"Module {Name}";
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
