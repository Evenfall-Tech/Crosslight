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
            var result = new ViewerNode(node);
            foreach (var child in node.Children)
            {
                result.Children.Add((Node)child.AcceptVisitor(this));
            }
            return result;
        }
    }
}
