using Crosslight.API.Nodes.Interfaces;
using Crosslight.API.Nodes.Metadata;
using Crosslight.API.Util;
using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    public abstract class Node : INode
    {
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
            throw new NotImplementedException();
        }
        public virtual S AcceptVisitor<S>(IVisitor<S> visitor)
        {
            throw new NotImplementedException();
        }
        public virtual S AcceptVisitor<T, S>(IVisitor<T, S> visitor, T data)
        {
            throw new NotImplementedException();
        }
    }
}
