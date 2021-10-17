using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;

namespace Crosslight.API.Nodes.Implementations.Entities
{
    /// <summary>
    /// <see cref="EnumMemberDeclarationNode"/> represents the enum member (None = 1) abstraction in the language.
    /// </summary>
    public class EnumMemberDeclarationNode : MemberDeclarationNode, IAttributesProvider, IModifiersProvider
    {
        // TODO: add value property
        public override string Type => nameof(EnumMemberDeclarationNode);
        public override string ToString()
        {
            return Type;
        }
    }
}
