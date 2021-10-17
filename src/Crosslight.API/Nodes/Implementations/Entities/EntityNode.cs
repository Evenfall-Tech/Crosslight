using Crosslight.API.Nodes.Implementations.Access.Modifiers;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Implementations.Entities
{
    /// <summary>
    /// <see cref="EntityNode"/> represents an entity declaration.
    /// It can be a C# class, struct, enum, delegate, etc.
    /// Similar to Member Declaration Syntax.
    /// </summary>
    public abstract class EntityNode : AttributedNode, IAttributedNode, IModifiedNode
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
