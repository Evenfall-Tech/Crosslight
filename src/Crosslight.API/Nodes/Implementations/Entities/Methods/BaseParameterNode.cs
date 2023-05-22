using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Nodes.Implementations.Access.Modifiers;
using Crosslight.API.Nodes.Implementations.Expressions.Types;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Methods
{
    public abstract class BaseParameterNode : Node, IAttributesProvider, IModifiersProvider
    {
        public override string Type => nameof(BaseParameterNode);
        public SyncedList<AttributeNode, Node> Attributes { get; }
        public SyncedList<ModifierNode, Node> Modifiers { get; }
        private readonly SyncedProperty<TypeReferenceNode, Node> declarationType;
        public TypeReferenceNode DeclarationType
        {
            get => declarationType.Value;
            set => declarationType.Value = value;
        }
        public BaseParameterNode()
        {
            Attributes = new SyncedList<AttributeNode, Node>(Children);
            Modifiers = new SyncedList<ModifierNode, Node>(Children);
            declarationType = new SyncedProperty<TypeReferenceNode, Node>(Children);
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
