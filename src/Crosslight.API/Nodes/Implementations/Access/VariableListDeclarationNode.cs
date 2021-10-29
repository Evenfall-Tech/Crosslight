using Crosslight.API.Nodes.Implementations.Expressions.Types;
using Crosslight.API.Nodes.Interfaces;
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

        public SyncedProperty<TypeReferenceNode, Node> DeclarationType { get; protected set; }
        public SyncedList<VariableDeclarationNode, Node> Variables { get; protected set; }
        public VariableListDeclarationNode()
        {
            DeclarationType = new SyncedProperty<TypeReferenceNode, Node>(Children);
            Variables = new SyncedList<VariableDeclarationNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
