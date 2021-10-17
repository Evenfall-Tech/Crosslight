using Crosslight.API.Nodes.Implementations;
using Crosslight.API.Nodes.Implementations.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Interfaces
{
    public interface IModifiedNode : INode
    {
        SyncedList<ModifierNode, Node> Modifiers { get; }
    }
}
