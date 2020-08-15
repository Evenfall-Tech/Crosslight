using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Crosslight.Viewer.ViewModels.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

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
            AddConnections(graphVM.GraphViewModel.Nodes);
            AddNodes(graphVM.GraphViewModel.Nodes);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void AddConnections(IEnumerable<NodeViewModel> nodes)
        {
            Random r = new Random(42);
            foreach (var node in nodes)
            {
                var relatives = nodes.Join(node.Connections, a => a.ID, b => b, (node, ind) => node);
                foreach (var rel in relatives)
                {
                    Line line = new Line()
                    {
                        StartPoint = new Point(node.Width / 2.0, node.Height / 2.0),
                        EndPoint = new Point(rel.Left + rel.Width / 2.0 - node.Left, rel.Top + rel.Height / 2.0 - node.Top),
                        StrokeThickness = 5.0,
                        Stroke = new SolidColorBrush(Color.FromRgb((byte)r.Next(), (byte)r.Next(), (byte)r.Next()), 1.0),
                    };
                    Canvas.SetLeft(line, node.Left);
                    Canvas.SetTop(line, node.Top);
                    canvas.Children.Add(line);
                }
            }
        }

        private void AddNodes(IEnumerable<NodeViewModel> nodes)
        {
            foreach (var node in nodes)
            {
                Control control = GraphNodeControlBuilder.GetGraphNodeControlFromNode(node);
                Canvas.SetLeft(control, node.Left);
                Canvas.SetTop(control, node.Top);
                canvas.Children.Add(control);
            }
        }
    }
}
