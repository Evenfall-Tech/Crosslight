using Crosslight.API.Nodes.Implementations.Expressions.Types;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities.Generics
{
    public class TypeConstraintNode : TemplateTypeParameterConstraintNode
    {
        public override string Type => nameof(TypeConstraintNode);
        private readonly SyncedProperty<TypeReferenceNode, Node> returnType;
        public TypeReferenceNode ReturnType
        {
            get => returnType.Value;
            set => returnType.Value = value;
        }
        public TypeConstraintNode()
        {
            returnType = new SyncedProperty<TypeReferenceNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
