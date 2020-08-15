using Crosslight.Viewer.Mock;
using Crosslight.Viewer.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class GraphViewerViewModel : ViewModelBase
    {
        public GraphViewerViewModel()
        {
            var ast = MockAST.CreateAST();
            var visitor = new GraphViewerVisitor();
            ast.AcceptVisitor(visitor);
            GraphViewModel = new GraphViewModel(visitor.Context);
            GraphViewModel.Sort(GraphNodeAlignment.Lowest, GraphNodeAlignment.Lowest);
        }

        public GraphViewModel GraphViewModel { get; }
    }
}
