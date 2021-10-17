using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities
{
    /// <summary>
    /// <see cref="EnumDeclarationNode"/> represents the enum abstraction in the language.
    /// </summary>
    public class EnumDeclarationNode : BaseTypeDeclarationNode, IIdentifierProvider, IAttributesProvider, IModifiersProvider
    {
        public override string Type => nameof(EnumDeclarationNode);
        public SyncedList<EnumMemberDeclarationNode, Node> Members { get; protected set; }
        public EnumDeclarationNode(string identifier) : base(identifier)
        {
            Members = new SyncedList<EnumMemberDeclarationNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
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
