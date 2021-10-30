using Crosslight.API.Nodes.Implementations.Expressions.Types;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Access
{
    /// <summary>
    /// <see cref="VariableListDeclarationNode"/> represents the variable abstraction in the language.
    /// It can declare one or more actual variables of a common type.
    /// </summary>
    public class VariableListDeclarationNode : Node
    {
        public override string Type => nameof(VariableListDeclarationNode);

        private readonly SyncedProperty<TypeReferenceNode, Node> declarationType;
        public TypeReferenceNode DeclarationType
        {
            get => declarationType.Value;
            set => declarationType.Value = value;
        }
        public SyncedList<VariableDeclarationNode, Node> Variables { get; protected set; }
        public VariableListDeclarationNode()
        {
            declarationType = new SyncedProperty<TypeReferenceNode, Node>(Children);
            Variables = new SyncedList<VariableDeclarationNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
