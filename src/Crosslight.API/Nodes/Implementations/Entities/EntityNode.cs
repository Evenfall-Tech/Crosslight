using Crosslight.API.Nodes.Implementations.Access;
using Crosslight.API.Nodes.Implementations.Access.Modifiers;
using Crosslight.API.Nodes.Interfaces.Access;
using Crosslight.API.Nodes.Interfaces.Access.Modifiers;
using Crosslight.API.Util;
using System;

namespace Crosslight.API.Nodes.Implementations.Entities
{
    /// <summary>
    /// <see cref="EntityNode"/> represents an entity declaration.
    /// It can be a C# class, struct, enum, delegate, etc.
    /// Similar to Member Declaration Syntax.
    /// </summary>
    [Obsolete("Use MemberDeclarationNode instead.")]
    public abstract class EntityNode : Node, IAttributesProvider, IModifiersProvider
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
            return Type;
        }
    }
}
