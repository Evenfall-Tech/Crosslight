using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Function
{
    public class GlobalFunctionNode : BaseFunctionNode, IIdentifierProvider, IModifiersProvider, IAttributesProvider/*, IFuncWithReturnTypeNode*/
    {
        public override string Type => nameof(GlobalFunctionNode);
        private readonly SyncedProperty<FunctionReturnTypeNode, Node> returnType;
        public FunctionReturnTypeNode ReturnType
        {
            get => returnType.Value;
            protected set => returnType.Value = value;
        }
        public GlobalFunctionNode(string name) : base(name)
        {
            returnType = new SyncedProperty<FunctionReturnTypeNode, Node>(Children);
        }
    }
}
