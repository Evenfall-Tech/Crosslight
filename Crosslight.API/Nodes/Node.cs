using Crosslight.API.Nodes.Metadata;
using Crosslight.API.Util;
using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    public abstract class Node
    {
        public virtual Node Parent { get; set; } = null;
        // TODO: remove this if it ends up unused.
        public SyncedList<MetadataNode, Node> Metadatas { get; protected set; }
        public virtual IList<Node> Children { get; protected set; } = null;
        public abstract string Type { get; }
        public Node()
        {
            Children = new List<Node>();
            Metadatas = new SyncedList<MetadataNode, Node>(Children);
        }
        public override string ToString()
        {
            return "Node";
        }
        public virtual object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
        public virtual S AcceptVisitor<S>(IVisitor<S> visitor)
        {
            return visitor.Visit(this);
        }
        public virtual S AcceptVisitor<T,S>(IVisitor<T,S> visitor, T data)
        {
            return visitor.Visit(this, data);
        }
    }
}
