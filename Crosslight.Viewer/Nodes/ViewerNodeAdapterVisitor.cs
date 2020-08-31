using Crosslight.API.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Viewer.Nodes
{
    public class ViewerNodeAdapterVisitor : IVisitor
    {
        public object Visit(Node node)
        {
            return new ViewerNode(node);
        }
    }
}
