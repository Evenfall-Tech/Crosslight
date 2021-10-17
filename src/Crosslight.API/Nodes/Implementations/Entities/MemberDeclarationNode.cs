using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Nodes.Implementations.Access.Modifiers;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities
{
    public abstract class MemberDeclarationNode : Node, IAttributesProvider, IModifiersProvider
    {
        public override string Type => nameof(FunctionEntityDeclarationNode);
        public SyncedList<AttributeNode, Node> Attributes { get; }
        public SyncedList<ModifierNode, Node> Modifiers { get; }

        public MemberDeclarationNode()
        {
            Attributes = new SyncedList<AttributeNode, Node>(Children);
            Modifiers = new SyncedList<ModifierNode, Node>(Children);
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
