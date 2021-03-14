using Crosslight.API.Nodes.Function;

namespace Crosslight.API.Nodes.Interfaces.Function
{
    public interface IFuncWithReturnTypeNode : INode
    {
        FunctionReturnTypeNode ReturnType { get; }
    }
}
