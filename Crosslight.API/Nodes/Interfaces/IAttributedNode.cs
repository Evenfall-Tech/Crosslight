using Crosslight.API.Nodes.Access;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Interfaces
{
    public interface IAttributedNode : INode
    {
        SyncedList<AttributeNode, Node> Attributes { get; }
    }
}
