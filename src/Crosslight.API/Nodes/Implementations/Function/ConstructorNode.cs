using Crosslight.API.Nodes.Implementations.Expressions;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Function
{
    public class ConstructorNode : BaseMethodNode, /*ITypeMemberNode, */IIdentifierProvider, IModifiersProvider, IAttributesProvider
    {
        public override string Type => nameof(ConstructorNode);
        public SyncedList<ExpressionNode, Node> Initializers { get; protected set; }
        public ConstructorNode(string identifier) : base(identifier)
        {
            Initializers = new SyncedList<ExpressionNode, Node>(Children);
        }
    }
}
