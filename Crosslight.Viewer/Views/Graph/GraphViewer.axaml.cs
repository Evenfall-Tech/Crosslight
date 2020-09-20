using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.Viewer.ViewModels.Graph;
using Crosslight.Viewer.Views.Utils;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.Viewer.Views.Graph
{
    public class GraphViewer : ReactiveUserControl<GraphViewerViewModel>
    {
        private Canvas Canvas => this.FindControl<Canvas>("graphCanvas");
        public GraphViewer()
        {
            this.WhenActivated(disposables =>
            {
                this.WhenAnyValue(x => x.ViewModel)
                    .Subscribe(x => UpdateCanvas(x))
                    .DisposeWith(disposables);
            });
            this.InitializeComponent();
        }

        private void UpdateCanvas(GraphViewerViewModel vm)
        {
            if (vm == null) return;
            Canvas.Children.Clear();
            if (vm.GraphViewModel == null) return;
            var cons = AddConnections(vm.GraphViewModel.Nodes);
            var nodes = AddNodes(vm.GraphViewModel.Nodes);
            UpdateNodesSize(nodes);
            vm.GraphViewModel.UpdateLayerAndNodePosition();
            Canvas.Children.AddRange(((IEnumerable<IControl>)cons).Concat(nodes));
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
                    ViewModel = new ConnectionViewModel(pair.From, pair.To),
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
                if (node.ViewModel != null)
                {
                    node.ViewModel.Width = size.Width;
                    node.ViewModel.Height = size.Height;
                }
            }
        }
    }
}
