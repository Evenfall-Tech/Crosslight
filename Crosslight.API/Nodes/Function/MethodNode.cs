using Crosslight.API.Nodes.Entities;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Function;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Function
{
    public class MethodNode : BaseMethodNode, ITypeMemberNode, INamedNode, IModifiedNode, IGenericDefinitionNode, IFuncWithReturnTypeNode
    {
        public override string Type => nameof(MethodNode);
        public SyncedList<TypeConstraintNode, Node> Constraints { get; protected set; }
        public SyncedList<TemplateTypeParameterNode, Node> TypeParameters { get; protected set; }
        FunctionalTypeNode ExplicitInterfaceSpecifier { get; set; }

        private readonly SyncedProperty<FunctionReturnTypeNode, Node> returnType;
        public FunctionReturnTypeNode ReturnType
        {
            get => returnType.Value;
            protected set => returnType.Value = value;
        }
        public MethodNode(string name) : base(name)
        {
            Constraints = new SyncedList<TypeConstraintNode, Node>(Children);
            TypeParameters = new SyncedList<TemplateTypeParameterNode, Node>(Children);
            returnType = new SyncedProperty<FunctionReturnTypeNode, Node>(Children);
        }
    }
}
