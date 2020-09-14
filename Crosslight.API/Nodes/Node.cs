using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    public abstract class Node
    {
        public virtual Node Parent { get; set; } = null;
        public virtual IList<Node> Children { get; protected set; } = null;
        public abstract string Type { get; }
        public Node()
        {
            Children = new List<Node>();
        }
        public override string ToString()
        {
            return "Node";
        }
        public virtual object AcceptVisitor(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
