using Crosslight.API.Nodes.Implementations.Entities;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;
using System.Collections.Generic;
using System.Linq;

namespace Crosslight.API.Nodes.Implementations.Componentization
{
    /// <summary>
    /// <see cref="NamespaceDeclarationNode"/> represents the namespace abstraction in the language.
    /// </summary>
    public class NamespaceDeclarationNode : MemberDeclarationNode, IIdentifierProvider, IAttributesProvider, IModifiersProvider
    {
        public override string Type => nameof(NamespaceDeclarationNode);
        public SyncedList<MemberDeclarationNode, Node> Members { get; protected set; }
        public IEnumerable<NamespaceDeclarationNode> Namespaces => Members.OfType<NamespaceDeclarationNode>();
        public string[] Identifiers { get; }
        public string Identifier => string.Join(".", Identifiers);
        public NamespaceDeclarationNode(string name) : this(name.Split('.'))
        { }
        public NamespaceDeclarationNode(IEnumerable<string> identifiers)
        {
            Members = new SyncedList<MemberDeclarationNode, Node>(Children);
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
