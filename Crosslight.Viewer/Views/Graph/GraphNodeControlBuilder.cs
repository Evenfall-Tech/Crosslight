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
            /*var color = style switch
            {
                GraphNodeControlStyle.Normal => Brushes.MidnightBlue,
                GraphNodeControlStyle.Info => Brushes.DeepSkyBlue,
                GraphNodeControlStyle.Warning => Brushes.DarkOrange,
                GraphNodeControlStyle.Danger => Brushes.DarkRed,
                _ => throw new NotImplementedException(),
            };
            if (!nodeVM.Active) color = new SolidColorBrush(
                Color.FromArgb(
                    color.Color.A, 
                    (byte)(color.Color.R / 2), 
                    (byte)(color.Color.G / 2), 
                    (byte)(color.Color.B / 2)
                ), color.Opacity);*/
            var view = new GraphNodeViewer()
            {
                ViewModel = nodeVM,
            };
            return view;
        }
    }
}
