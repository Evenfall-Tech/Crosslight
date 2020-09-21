using Avalonia;
using Avalonia.Media;
using Crosslight.Viewer.ViewModels.Graph;
using System;

namespace Crosslight.Viewer.Views.Graph
{
    public static class GraphNodeControlBuilder
    {
        public enum GraphNodeControlStyle
        {
            Normal,
            Info,
            Warning,
            Danger,
        }

        public static GraphNodeViewer BuildGraphNodeControl(NodeViewModel nodeVM, GraphNodeControlStyle style = GraphNodeControlStyle.Normal)
        {
            var color = style switch
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
                ), color.Opacity);
            var view = new GraphNodeViewer()
            {
                ViewModel = nodeVM,
                ChildBorderBrush = color,
                ChildBorderThickness = new Thickness(2.0, 2.0),
                ChildPadding = new Thickness(5.0),
                ChildBackground = nodeVM.Active ? Brushes.White : Brushes.LightGray,
            };
            return view;
        }
    }
}
