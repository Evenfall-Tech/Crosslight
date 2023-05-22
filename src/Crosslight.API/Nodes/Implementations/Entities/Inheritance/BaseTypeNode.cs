using Crosslight.API.Nodes.Implementations.Expressions.Types;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Inheritance
{
    /// <summary>
    /// <see cref="BaseTypeNode"/> node represents a base type reference
    /// in context of inheritance.
    /// </summary>
    public class BaseTypeNode : Node
    {
        public override string Type => nameof(BaseTypeNode);
        public SyncedProperty<TypeReferenceNode, Node> BaseType { get; protected set; }
        public BaseTypeNode(string identifier)
        {
            // TODO: implement this for class parents and constructor base
            BaseType = new SyncedProperty<TypeReferenceNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
