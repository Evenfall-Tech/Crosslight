using Crosslight.API.Nodes.Access;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// <see cref="EntityNode"/> represents an entity declaration.
    /// It can be a C# class, struct, enum, delegate, etc.
    /// </summary>
    public abstract class EntityNode : AttributedNode
    {
        public override string Type => nameof(EntityNode);
        public SyncedList<ModifierNode, Node> Modifiers { get; protected set; }
        public EntityNode()
        {
            Modifiers = new SyncedList<ModifierNode, Node>(Children);
        }
        public override string ToString()
        {
            return nameof(EntityNode);
        }
    }
}
