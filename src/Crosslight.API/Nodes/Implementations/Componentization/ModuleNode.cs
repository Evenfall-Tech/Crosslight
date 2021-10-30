using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Componentization
{
    /// <summary>
    /// <see cref="ModuleNode"/> represents the module abstraction in the language.
    /// E.g. in C# it is module, for Java it is package,
    /// in C++ it is library.
    /// </summary>
    public class ModuleNode : Node, IIdentifierProvider, IAttributesProvider
    {
        public override string Type => nameof(ModuleNode);
        public string Identifier { get; }
        public SyncedList<AttributeNode, Node> Attributes { get; protected set; }
        public SyncedList<NamespaceDeclarationNode, Node> Namespaces { get; protected set; }

        public ModuleNode(string identifier)
        {
            Attributes = new SyncedList<AttributeNode, Node>(Children);
            Namespaces = new SyncedList<NamespaceDeclarationNode, Node>(Children);
            Identifier = identifier;
        }
        public override string ToString()
        {
            return $"Module {Identifier}";
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
