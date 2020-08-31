using Avalonia.Controls;
using Crosslight.API.IO;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.CIL.Lang;
using Crosslight.Viewer.Mock;
using Crosslight.Viewer.Nodes;

namespace Crosslight.Viewer.ViewModels.Graph
{
    public class GraphViewerViewModel : ViewModelBase
    {
        public GraphViewerViewModel(string[] assemblies)
        {
            Source source = Source.FromFiles(assemblies);

            CrosslightContext context = new CrosslightContext()
            {
                InputLanguage = new CILInputLanguage(),
                OutputLanguage = null,
            };

            Node ast = context.InputLanguage.Decode(source);
            if (ast != null)
            {
                ast = (ViewerNode)ast.AcceptVisitor(new ViewerNodeAdapterVisitor());
            }

            var visitor = new GraphViewerVisitor();
            _ = ast.AcceptVisitor(visitor);
            GraphViewModel = new GraphViewModel(visitor.Context, GraphNodeDirection.Right);
            GraphViewModel.Sort(GraphNodeAlignment.Lowest, GraphNodeAlignment.Lowest);
        }

        public GraphViewModel GraphViewModel { get; }
    }
}
