using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Function;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Function
{
    public class OperatorFunctionNode : BaseMethodNode, ITypeMemberNode, INamedNode, IModifiedNode, IFuncWithReturnTypeNode
    {
        public override string Type => nameof(OperatorFunctionNode);
        private readonly SyncedProperty<FunctionReturnTypeNode, Node> returnType;
        private readonly SyncedProperty<OperatorNode, Node> operatorToken;
        public FunctionReturnTypeNode ReturnType
        {
            get => returnType.Value;
            protected set => returnType.Value = value;
        }
        public OperatorNode OperatorToken
        {
            get => operatorToken.Value;
            protected set => operatorToken.Value = value;
        }
        public OperatorFunctionNode(string name) : base(name)
        {
            operatorToken = new SyncedProperty<OperatorNode, Node>(Children);
            returnType = new SyncedProperty<FunctionReturnTypeNode, Node>(Children);
        }
    }
}
