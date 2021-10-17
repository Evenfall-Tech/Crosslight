using Avalonia;
using Avalonia.Media;
using Crosslight.Language.Viewer.ViewModels.Graph;
using System;

namespace Crosslight.Language.Viewer.Views.Graph
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
