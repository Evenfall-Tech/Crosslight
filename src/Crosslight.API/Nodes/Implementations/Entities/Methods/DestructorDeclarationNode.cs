using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;

namespace Crosslight.API.Nodes.Implementations.Entities.Methods
{
    public class DestructorDeclarationNode : BaseMethodDeclarationNode, IIdentifierProvider, IModifiersProvider, IAttributesProvider
    {
        public override string Type => nameof(DestructorDeclarationNode);
        public string Identifier { get; }
        public DestructorDeclarationNode(string identifier)
        {
            Identifier = identifier;
        }
    }
}
