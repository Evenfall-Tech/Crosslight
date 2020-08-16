using System;
using System.Collections.Generic;

namespace Crosslight.API.Nodes
{
    public abstract class Node
    {
        public virtual Node Parent { get; protected set; } = null;
        public virtual List<Node> Children { get; protected set; } = null;
        public override string ToString()
        {
            return "Node";
        }
        public virtual object AcceptVisitor<TVisitor>(TVisitor visitor) where TVisitor : IVisitor
        {
            return visitor.Visit(this);
        }
    }
}
