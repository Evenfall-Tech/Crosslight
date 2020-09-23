using Crosslight.API.Nodes.Access;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Entities
{
    public abstract class AttributedNode : Node
    {
        public override string Type => nameof(AttributedNode);
        public SyncedList<AttributeNode, Node> Attributes { get; protected set; }
        public AttributedNode()
        {
            Attributes = new SyncedList<AttributeNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
