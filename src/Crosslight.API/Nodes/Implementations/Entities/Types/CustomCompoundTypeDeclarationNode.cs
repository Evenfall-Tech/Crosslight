using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Nodes.Interfaces.Entities;

namespace Crosslight.API.Nodes.Implementations.Entities.Types
{
    /// <summary>
    /// <see cref="CustomCompoundTypeDeclarationNode"/> is reserved for custom type declarations from other languages.
    /// E.g. Java enum, etc.
    /// </summary>
    public class CustomCompoundTypeDeclarationNode : CompoundTypeDeclarationNode, IAttributesProvider, IModifiersProvider, IGenericDefinitionProvider, IIdentifierProvider
    {
        public override string Type => nameof(CustomCompoundTypeDeclarationNode);
        public string Keyword { get; }
        public CustomCompoundTypeDeclarationNode(string keyword, string identifier) : base(identifier)
        {
            Keyword = keyword;
        }
        public override string ToString()
        {
            return $"{Keyword} {Identifier}";
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
