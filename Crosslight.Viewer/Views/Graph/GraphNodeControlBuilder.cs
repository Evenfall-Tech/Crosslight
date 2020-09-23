using Avalonia;
using Avalonia.Media;
using Crosslight.Viewer.ViewModels.Graph;
using System;

namespace Crosslight.Viewer.Views.Graph
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
