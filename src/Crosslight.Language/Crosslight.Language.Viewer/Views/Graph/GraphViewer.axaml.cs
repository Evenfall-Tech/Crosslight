using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.Language.Viewer.ViewModels.Graph;
using Crosslight.Language.Viewer.Views.Utils;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.Language.Viewer.Views.Graph
{
    public class GraphViewer : ReactiveUserControl<GraphViewerViewModel>
    {
        private Canvas Canvas => this.FindControl<Canvas>("graphCanvas");
        private RoutedViewHost RVH => this.FindControl<RoutedViewHost>("rvh");
        private Button NavBack => this.FindControl<Button>("navBack");
        private Button NavForw => this.FindControl<Button>("navForw");
        private TextBlock NavName => this.FindControl<TextBlock>("navName");
        public GraphViewer()
        {
            Splat.Locator.CurrentMutable.Register(() => new GraphRoutable(), typeof(IViewFor<GraphRoutableViewModel>));
            this.WhenActivated(disposables =>
            {
                this.WhenAnyValue(x => x.ViewModel, x => x.ViewModel.GraphViewModel.Nodes)
                    .Subscribe(x =>
                    {
                        x.Item1.GraphViewModel.Sort();
                        UpdateCanvas(x.Item1);
                    })
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, x => x.Router, x => x.RVH.Router)
                    .DisposeWith(disposables);
                this.BindCommand(ViewModel, x => x.NavigateBackward, x => x.NavBack)
                    .DisposeWith(disposables);
                this.BindCommand(ViewModel, x => x.NavigateForward, x => x.NavForw)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, x => x.GraphViewModel.StartNode.Data, x => x.NavName.Text)
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
            Canvas.Children.AddRange(((IEnumerable<IControl>)cons).Concat(nodes));
            UpdateNodesSize(nodes);
            vm.GraphViewModel.UpdateLayerAndNodePosition();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private IEnumerable<GraphConnectionViewer> AddConnections(IEnumerable<NodeViewModel> nodes)
        {
            List<IControl> result = new List<IControl>();

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
