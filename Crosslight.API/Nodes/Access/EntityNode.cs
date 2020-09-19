using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Access
{
    public abstract class EntityNode : Node
    {
        public override string Type => nameof(EntityNode);
        public SyncedList<AttributeNode, Node> Attributes { get; protected set; }
        public SyncedList<ModifierNode, Node> Modifiers { get; protected set; }
        public EntityNode()
        {
            Attributes = new SyncedList<AttributeNode, Node>(Children);
            Modifiers = new SyncedList<ModifierNode, Node>(Children);
        }
        public override string ToString()
        {
            return nameof(EntityNode);
        }
    }
}
