using Crosslight.API.Nodes;
using Crosslight.Viewer.Nodes;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class GraphViewerViewModel : ViewModelBase
    {
        public GraphViewerViewModel(Node ast)
        {
            //ViewerNode viewerNode;
            //viewerNode = (ViewerNode)ast.AcceptVisitor(new ViewerNodeAdapterVisitor());

            var visitor = new GraphViewerVisitor();
            _ = ast.AcceptVisitor(visitor);
            graphViewModel = new GraphViewModel(visitor.Context, GraphNodeDirection.Right);
            graphViewModel.Sort(GraphNodeAlignment.Lowest, GraphNodeAlignment.Lowest);
            OnPropertyChanged(nameof(GraphViewModel));
        }

        private GraphViewModel graphViewModel;

        public GraphViewModel GraphViewModel
        {
            get => graphViewModel;
            set
            {
                graphViewModel = value;
                OnPropertyChanged(nameof(GraphViewModel));
            }
        }
    }
}
