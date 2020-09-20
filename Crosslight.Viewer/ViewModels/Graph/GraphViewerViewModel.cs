using Crosslight.API.Nodes;
using Crosslight.Viewer.Nodes;
using ReactiveUI;
using System;
using System.Reactive.Disposables;

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
                    .Subscribe(x => BuildGraphFromNode(x))
                    .DisposeWith(disposables);
            });
        }

        private void BuildGraphFromNode(Node node)
        {
            //ViewerNode viewerNode;
            //viewerNode = (ViewerNode)ast.AcceptVisitor(new ViewerNodeAdapterVisitor());
            var visitor = new GraphViewerVisitor();
            _ = rootNode.AcceptVisitor(visitor);
            graphViewModel = new GraphViewModel(visitor.Context, GraphNodeDirection.Right);
            graphViewModel.Sort(GraphNodeAlignment.Lowest, GraphNodeAlignment.Lowest);
            this.RaisePropertyChanged(nameof(GraphViewModel));
        }
    }
}
