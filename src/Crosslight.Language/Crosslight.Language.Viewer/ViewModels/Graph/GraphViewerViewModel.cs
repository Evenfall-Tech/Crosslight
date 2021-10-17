using Crosslight.API.Nodes;
using Crosslight.Language.Viewer.Models.Graph;
using Crosslight.Language.Viewer.Nodes;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.Language.Viewer.ViewModels.Graph
{
    public class GraphViewerViewModel : ViewModelBase, IActivatableViewModel, IScreen
    {
        private Node rootNode;
        private GraphViewModel graphViewModel;
        private readonly LinkedList<NodeModel> navigationStack;
        private LinkedListNode<NodeModel> navigationCurrent;
        private const string InternalNavigationChanged = "InternalNavigationChanged";

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
        public RoutingState Router { get; } = new RoutingState();
        public ReactiveCommand<Unit, IRoutableViewModel> NavigateForward { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> NavigateBackward { get; }
        public ReactiveCommand<NodeModel, IRoutableViewModel> NavigateToNode { get; }

        public GraphViewerViewModel()
        {
            navigationStack = new LinkedList<NodeModel>();

            Activator = new ViewModelActivator();

            var navigationChanged = this
                .WhenAnyPropertyChanged(InternalNavigationChanged);
            NavigateToNode = ReactiveCommand.CreateFromObservable(
                (NodeModel m) =>
                {
                    if (navigationCurrent?.Next?.Value == m)
                    {
                        return NavigateForward.Execute();
                    }

                    while (navigationStack.Count > 0 && navigationStack.Last != navigationCurrent)
                        navigationStack.RemoveLast();
                    navigationStack.AddLast(m);
                    navigationCurrent = navigationStack.Last;
                    if (navigationStack.Count > GraphViewModelOptions.NavigationStackSize) navigationStack.RemoveFirst();
                    this.RaisePropertyChanged(InternalNavigationChanged);
                    return Router.Navigate.Execute(new GraphRoutableViewModel(m, this));
                }
            );
            NavigateBackward = ReactiveCommand.CreateFromObservable(
                () => Observable.Defer(
                () =>
                {
                    navigationCurrent = navigationCurrent.Previous;
                    this.RaisePropertyChanged(InternalNavigationChanged);
                    return Router.NavigateBack.Execute().Select<Unit, IRoutableViewModel>(x => null);
                }),
                navigationChanged.Select(_ => 
                    navigationStack.Count > 1 && 
                    navigationCurrent != null && 
                    navigationCurrent.Previous != null
                )
            );
            NavigateForward = ReactiveCommand.CreateFromObservable(
                () =>
                {
                    navigationCurrent = navigationCurrent.Next;
                    this.RaisePropertyChanged(InternalNavigationChanged);
                    return Router.Navigate.Execute(new GraphRoutableViewModel(navigationCurrent.Value, this));
                },
                navigationChanged.Select(_ => navigationCurrent != null && navigationCurrent.Next != null)
            );

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                this.WhenAnyValue(x => x.RootNode)
                    .DistinctUntilChanged()
                    .Subscribe(x => BuildGraphFromNode())
                    .DisposeWith(disposables);
                GraphViewModel
                    .WhenAnyValue(x => x.StartNode)
                    .DistinctUntilChanged()
                    .Where(x => x != navigationCurrent.Value)
                    .InvokeCommand(NavigateToNode)
                    .DisposeWith(disposables);
                Router.CurrentViewModel
                    .Where(x => x is GraphRoutableViewModel)
                    .Subscribe(x => GraphViewModel.StartNode = (x as GraphRoutableViewModel).Node)
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
            navigationStack.Clear();
            navigationStack.AddLast(graphViewModel.StartNode);
            navigationCurrent = navigationStack.Last;
            Router.NavigateAndReset.Execute(new GraphRoutableViewModel(navigationCurrent.Value, this));
            this.RaisePropertyChanged(nameof(GraphViewModel));
        }
    }
}
