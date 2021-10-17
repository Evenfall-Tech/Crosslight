using Crosslight.API.Nodes.Implementations.Entities;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Nodes.Interfaces.Entities;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Function
{
    public class MethodNode : BaseMethodNode, /*ITypeMemberNode, */IIdentifierProvider, IModifiersProvider, IAttributesProvider, IGenericDefinitionProvider/*, IFuncWithReturnTypeNode*/
    {
        // TODO: fix this mess
        public override string Type => nameof(MethodNode);
        public SyncedList<TypeConstraintNode, Node> Constraints { get; protected set; }
        public SyncedList<TemplateTypeParameterNode, Node> TypeParameters { get; protected set; }
        FunctionalTypeDeclarationNode ExplicitInterfaceSpecifier { get; set; }
        private readonly SyncedProperty<FunctionReturnTypeNode, Node> returnType;
        public FunctionReturnTypeNode ReturnType
        {
            get => returnType.Value;
            protected set => returnType.Value = value;
        }
        public int Arity => throw new System.NotImplementedException();

        public MethodNode(string name) : base(name)
        {
            Constraints = new SyncedList<TypeConstraintNode, Node>(Children);
            TypeParameters = new SyncedList<TemplateTypeParameterNode, Node>(Children);
            returnType = new SyncedProperty<FunctionReturnTypeNode, Node>(Children);
        }
    }
}
