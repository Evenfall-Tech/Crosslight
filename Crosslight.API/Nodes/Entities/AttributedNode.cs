using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// <see cref="AttributedNode"/> represents a node containing attributes.
    /// </summary>
    public abstract class AttributedNode : Node, IAttributedNode
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
