using Crosslight.API.Nodes.Access;
using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Entities
{
    /// <summary>
    /// <see cref="CompoundTypeNode"/> abstract node represents types
    /// that can have inner declarations.
    /// </summary>
    public abstract class CompoundTypeNode : FunctionalTypeNode, IAttributedNode, IModifiedNode, IGenericDefinitionNode, INamedNode
    {
        public override string Type => nameof(CompoundTypeNode);
        public SyncedList<FieldNode, Node> Fields { get; protected set; }
        public SyncedList<EntityNode, Node> InnerEntities { get; protected set; }
        public CompoundTypeNode(string name) : base(name)
        {
            Fields = new SyncedList<FieldNode, Node>(Children);
            InnerEntities = new SyncedList<EntityNode, Node>(Children);
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
