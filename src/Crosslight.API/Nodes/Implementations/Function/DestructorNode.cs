using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;

namespace Crosslight.API.Nodes.Implementations.Function
{
    public class DestructorNode : BaseMethodNode, /*ITypeMemberNode, */IIdentifierProvider, IModifiersProvider, IAttributesProvider
    {
        public override string Type => nameof(DestructorNode);
        public DestructorNode(string name) : base(name)
        {
        }
    }
}
