using Crosslight.API.Nodes.Implementations;
using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Interfaces.Access
{
    public interface IAttributesProvider
    {
        SyncedList<AttributeNode, Node> Attributes { get; }
    }
}
