using Crosslight.API.Nodes.Implementations;
using Crosslight.API.Nodes.Implementations.Access.Modifiers;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Interfaces.Access.Modifiers
{
    public interface IModifiersProvider
    {
        SyncedList<ModifierNode, Node> Modifiers { get; }
    }
}
