using Crosslight.API.Nodes.Access;
using Crosslight.API.Util;

namespace Crosslight.API.Nodes.Entities
{
    public class BaseTypeNode : EntityNode
    {
        public override string Type => nameof(TypeNode);
        public SyncedList<FieldNode, Node> Fields { get; protected set; }
        public SyncedList<EntityNode, Node> InnerEntities { get; protected set; }
        public BaseTypeNode()
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
