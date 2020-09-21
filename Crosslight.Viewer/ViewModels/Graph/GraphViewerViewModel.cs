using Crosslight.API.Nodes;
using Crosslight.Viewer.Nodes;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class GraphViewerViewModel : ViewModelBase, IActivatableViewModel
    {
        private Node rootNode;
        private GraphViewModel graphViewModel;

        public ViewModelActivator Activator { get; }
        public GraphViewModel GraphViewModel
        {
            get => graphViewModel;
            set => this.RaiseAndSetIfChanged(ref graphViewModel, value);
        }
        public Node RootNode
        {
            get => rootNode;
            set => this.RaiseAndSetIfChanged(ref rootNode, value);
        }

        public GraphViewerViewModel()
        {
            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                this.WhenAnyValue(x => x.RootNode)
                    .DistinctUntilChanged()
                    .Subscribe(x => BuildGraphFromNode())
                    .DisposeWith(disposables);
            });
        }

        private void BuildGraphFromNode()
        {
            //ViewerNode viewerNode;
            //viewerNode = (ViewerNode)ast.AcceptVisitor(new ViewerNodeAdapterVisitor());
            var visitor = new GraphViewerVisitor();
            _ = rootNode.AcceptVisitor(visitor);
            graphViewModel = new GraphViewModel(visitor.Context, visitor.Context.Nodes.Values.FirstOrDefault(), 
                new GraphViewModelOptions()
                {
                    HorizontalAlignment = GraphNodeAlignment.Lowest,
                    VerticalAlignment = GraphNodeAlignment.Lowest,
                    NodeDirection = GraphNodeDirection.Right,
                });
            graphViewModel.Sort();
            this.RaisePropertyChanged(nameof(GraphViewModel));
        }
    }
}
