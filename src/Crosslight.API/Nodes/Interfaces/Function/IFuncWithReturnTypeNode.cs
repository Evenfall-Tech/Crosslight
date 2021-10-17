using Crosslight.API.Nodes.Implementations.Function;

namespace Crosslight.API.Nodes.Interfaces.Function
{
    public interface IFuncWithReturnTypeNode : INode
    {
        FunctionReturnTypeNode ReturnType { get; }
    }
}
