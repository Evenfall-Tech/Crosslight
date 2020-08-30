using Crosslight.API.Lang;
using Crosslight.Viewer.Mock;
using Crosslight.Viewer.Nodes;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class GraphViewerViewModel : ViewModelBase
    {
        public GraphViewerViewModel()
        {
            ViewerNode ast = MockAST.CreateAST();
            var visitor = new GraphViewerVisitor();
            _ = ast.AcceptVisitor(visitor);
            GraphViewModel = new GraphViewModel(visitor.Context, GraphNodeDirection.Right);
            GraphViewModel.Sort(GraphNodeAlignment.Lowest, GraphNodeAlignment.Lowest);
        }

        public GraphViewModel GraphViewModel { get; }
    }
}
