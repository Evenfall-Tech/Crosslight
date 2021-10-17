using Crosslight.API.Nodes.Implementations.Entities;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;

namespace Crosslight.API.Nodes.Implementations.Access
{
    /// <summary>
    /// <see cref="ValueNode"/> represents the constant, literal or variable.
    /// </summary>
    public abstract class ValueNode : MemberDeclarationNode, IAttributesProvider, IModifiersProvider
    {
        public override string Type => nameof(ValueNode);
        /// <summary>
        /// ValueType is needed to keep the type of the value.
        /// Concrete implementations may also keep the name (like name
        /// of the variable) or the value (like value of the literal).
        /// </summary>
        // TODO: rewrite with a reference.
        public EntityNode ValueType { get; protected set; }
        public ValueNode()
        {
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
