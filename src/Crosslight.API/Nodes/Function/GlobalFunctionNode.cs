using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Function;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Function
{
    public class GlobalFunctionNode : BaseFunctionNode, INamedNode, IModifiedNode, IFuncWithReturnTypeNode
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
