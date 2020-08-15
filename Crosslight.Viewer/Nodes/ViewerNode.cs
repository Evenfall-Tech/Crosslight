using Crosslight.API.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crosslight.Viewer.Nodes
{
    public class ViewerNode : Node
    {
        public void SetChildren(IEnumerable<Node> nodes) => this.Children = nodes.ToList();
        public void SetParent(Node parent) => this.Parent = parent;
    }
}
