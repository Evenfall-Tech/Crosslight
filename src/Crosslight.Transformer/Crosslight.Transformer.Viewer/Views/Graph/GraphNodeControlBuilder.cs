using Crosslight.Transformer.Viewer.ViewModels.Graph;

namespace Crosslight.Transformer.Viewer.Views.Graph
{
    public static class GraphNodeControlBuilder
    {
        public static GraphNodeViewer BuildGraphNodeControl(NodeViewModel nodeVM)
        {
            var view = new GraphNodeViewer()
            {
                ViewModel = nodeVM,
            };
            return view;
        }
    }
}
