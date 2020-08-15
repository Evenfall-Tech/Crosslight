using Avalonia;
using Avalonia.Controls;
using Crosslight.API.Nodes;
using Crosslight.Viewer.Models.Graph;
using Crosslight.Viewer.ViewModels.Graph;
using Crosslight.Viewer.ViewModels.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Crosslight.Viewer.Nodes
{
    public class GraphViewerVisitor : IVisitor
    {
        public GraphModel Context { get; private set; }
        private int idGen;

        public GraphViewerVisitor()
        {
            Context = new GraphModel
            {
                Nodes = new List<NodeModel>()
            };
            idGen = 0;
        }
        public object Visit(Node node)
        {
            if (node is ViewerNode target) return Visit(target);
            return null;
        }

        public NodeModel Visit(ViewerNode node)
        {
            string name = node.ToString() + idGen.ToString();
            var parent = new NodeModel()
            {
                ID = idGen++,
                Data = BuildNodeControl(name),
                Connections = new List<int>(),
            };
            Context.Nodes.Add(parent);
            if (node.Children != null)
            {
                for (int i = 0; i < node.Children.Count; ++i)
                {
                    var child = Visit((ViewerNode)node.Children[i]);
                    child.Connections = new int[] { parent.ID };
                    parent.Connections.Append(child.ID);
                }
            }
            return parent;
        }

        //TODO: in case this doesn't get refactored, replace with NodeModel or AST Node
        private ControlWrapper BuildNodeControl(string data)
        {
            var text = new TextBlock()
            {
                Text = data
            };
            var border = new Border()
            {
                BorderBrush = Avalonia.Media.Brushes.DarkGray,
                BorderThickness = new Thickness(1.0, 1.0),
                CornerRadius = new CornerRadius(2.5),
                Child = text,
                Padding = new Thickness(5.0),
            };
            return new ControlWrapper(border, GraphNodeDirection.DownRight);
        }
    }
}
