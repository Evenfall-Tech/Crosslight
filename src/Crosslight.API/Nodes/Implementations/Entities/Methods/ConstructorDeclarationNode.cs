using Crosslight.API.Nodes.Implementations.Expressions;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Methods
{
    public class ConstructorDeclarationNode : BaseMethodDeclarationNode, IIdentifierProvider, IModifiersProvider, IAttributesProvider
    {
        public override string Type => nameof(ConstructorDeclarationNode);
        // Should take care of languages where variables can be initialized, not only base and this.
        public SyncedList<ExpressionNode, Node> Initializers { get; protected set; }
        public string Identifier { get; }
        public ConstructorDeclarationNode(string identifier)
        {
            Identifier = identifier;
            Initializers = new SyncedList<ExpressionNode, Node>(Children);
        }
    }
}
