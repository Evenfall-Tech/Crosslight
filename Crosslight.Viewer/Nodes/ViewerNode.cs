using Crosslight.API.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crosslight.Viewer.Nodes
{
    public class ViewerNode : Node
    {
        public Node Content { get; set; }

        public override Type Type => typeof(ViewerNode);

        public ViewerNode(Node content)
        {
            Content = content;
        }
        public void SetChildren(IEnumerable<Node> nodes) => Children = nodes.ToList();
        public void SetParent(Node parent) => this.Parent = parent;
        public object AcceptVisitor(IViewerVisitor visitor)
        {
            return visitor.Visit(this);
        }
        public override string ToString() => Content?.ToString();
    }
}
