using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;

namespace Crosslight.API.Nodes.Implementations.Entities.Fields
{
    /// <summary>
    /// <see cref="FieldDeclarationNode"/> represents a field declaration.
    /// </summary>
    public class FieldDeclarationNode : BaseFieldDeclarationNode, IAttributesProvider, IModifiersProvider
    {
        public override string Type => nameof(FieldDeclarationNode);

        public override string ToString()
        {
            return Type;
        }
    }
}
