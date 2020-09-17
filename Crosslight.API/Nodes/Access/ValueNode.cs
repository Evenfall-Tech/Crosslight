using System;

namespace Crosslight.API.Nodes.Access
{
    /// <summary>
    /// ValueNode represents the constant, literal or variable.
    /// </summary>
    public abstract class ValueNode : Node
    {
        public override string Type => nameof(ValueNode);
        /// <summary>
        /// ValueType is needed to keep the type of the value.
        /// Concrete implementations may also keep the name (like name
        /// of the variable) or the value (like value of the literal).
        /// </summary>
        public TypeNode ValueType { get; protected set; }
        public ValueNode()
        {
        }
        public override string ToString()
        {
            return "ValueNode";
        }
    }
}
