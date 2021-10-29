using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;

namespace Crosslight.API.Nodes.Implementations.Entities.Fields
{
    /// <summary>
    /// <see cref="EventFieldDeclarationNode"/> represents an event field declaration.
    /// </summary>
    public class EventFieldDeclarationNode : BaseFieldDeclarationNode, IAttributesProvider, IModifiersProvider
    {
        public override string Type => nameof(EventFieldDeclarationNode);

        public override string ToString()
        {
            return Type;
        }
    }
}
