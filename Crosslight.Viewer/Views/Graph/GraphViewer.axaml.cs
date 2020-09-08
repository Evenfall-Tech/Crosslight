using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Crosslight.Viewer.ViewModels.Graph;
using Crosslight.Viewer.Views.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Crosslight.Viewer.Views.Graph
{
    public class GraphViewer : UserControl
    {
        private readonly Canvas canvas;
        public GraphViewer()
        {
            this.InitializeComponent();

            canvas = this.FindControl<Canvas>("graphCanvas");
            DataContextChanged += GraphViewer_DataContextChanged;
        }

        private void GraphViewer_DataContextChanged(object sender, System.EventArgs e)
        {
            if (!(DataContext is GraphViewerViewModel graphVM)) return;
            canvas.Children.Clear();
            var cons = AddConnections(graphVM.GraphViewModel.Nodes);
            var nodes = AddNodes(graphVM.GraphViewModel.Nodes);
            UpdateNodesSize(nodes);
            graphVM.GraphViewModel.UpdateLayerAndNodePosition();
            canvas.Children.AddRange(((IEnumerable<IControl>)cons).Concat(nodes));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private IEnumerable<GraphConnectionViewer> AddConnections(IEnumerable<NodeViewModel> nodes)
        {
            List<IControl> result = new List<IControl>();
            Random r = new Random(42);

            return nodes
                .SelectMany(
                    node => nodes.Join(node.Connections, a => a.ID, b => b, (node, ind) => node),
                    (node, rel) => new { From = node, To = rel }
                )
                .Select(pair => new GraphConnectionViewer()
                {
                    DataContext = new ConnectionViewModel(pair.From, pair.To),
                });
        }

        private IEnumerable<GraphNodeViewer> AddNodes(IEnumerable<NodeViewModel> nodes)
        {
            return nodes.Select(node =>
            {
                GraphNodeViewer control = GraphNodeControlBuilder.BuildGraphNodeControl(node);
                return control;
            }).ToList();
        }

        private void UpdateNodesSize(IEnumerable<GraphNodeViewer> nodes)
        {
            foreach (var node in nodes)
            {
                var size = SizeMeasures.GetMinControlSize(node);
                if (node.DataContext is NodeViewModel nodeVM)
                {
                    nodeVM.Width = size.Width;
                    nodeVM.Height = size.Height;
                }
            }
        }
    }
}
