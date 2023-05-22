using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Nodes.Interfaces.Entities;

namespace Crosslight.API.Nodes.Implementations.Entities.Types
{
    /// <summary>
    /// <see cref="ClassDeclarationNode"/> represents the class abstraction in the language.
    /// </summary>
    public class ClassDeclarationNode : CompoundTypeDeclarationNode, IAttributesProvider, IModifiersProvider, IGenericDefinitionProvider, IIdentifierProvider
    {
        public override string Type => nameof(ClassDeclarationNode);
        public ClassDeclarationNode(string identifier) : base(identifier)
        {
        }
        public override string ToString()
        {
            return $"Class {Identifier}";
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
