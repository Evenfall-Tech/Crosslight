using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Interfaces.Function;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Function
{
    public class ConversionOperatorFunctionNode : BaseMethodNode, ITypeMemberNode, INamedNode, IModifiedNode, IFuncWithReturnTypeNode
    {
        public override string Type => nameof(ConversionOperatorFunctionNode);
        SyncedProperty<ImplicitOrExplicitKeywordNode, Node> Keyword { get; set; }

        private readonly SyncedProperty<FunctionReturnTypeNode, Node> returnType;
        public FunctionReturnTypeNode ReturnType
        {
            get => returnType.Value;
            protected set => returnType.Value = value;
        }
        public ConversionOperatorFunctionNode(string name) : base(name)
        {
            Keyword = new SyncedProperty<ImplicitOrExplicitKeywordNode, Node>(Children);
            returnType = new SyncedProperty<FunctionReturnTypeNode, Node>(Children);
        }
    }
}
