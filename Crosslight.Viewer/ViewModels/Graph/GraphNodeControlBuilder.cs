using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Crosslight.Viewer.Views.Utils;
using System;

namespace Crosslight.Viewer.ViewModels.Graph
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
        public static Control GetGraphNodeControlFromNode(NodeViewModel node)
        {
            if (node == null || node.Data == null) return null;
            if (node.Data is ControlWrapper wrapper)
            {
                return wrapper.Control;
            }
            else if (node.Data is Control ctrl)
            {
                return ctrl;
            }
            else
            {
                return BuildGraphNodeControl(node.Data.ToString(), GraphNodeControlStyle.Warning).Control;
            }
        }

        public static ControlWrapper BuildGraphNodeControl(string data, GraphNodeControlStyle style = GraphNodeControlStyle.Normal)
        {
            var text = new TextBlock()
            {
                Text = data,
            };
            var color = style switch
            {
                GraphNodeControlStyle.Normal => Brushes.DarkGray,
                GraphNodeControlStyle.Info => Brushes.DeepSkyBlue,
                GraphNodeControlStyle.Warning => Brushes.DarkOrange,
                GraphNodeControlStyle.Danger => Brushes.DarkRed,
                _ => throw new NotImplementedException(),
            };
            var border = new Border()
            {
                BorderBrush = color,
                BorderThickness = new Thickness(2.0, 2.0),
                CornerRadius = new CornerRadius(5.0),
                Child = text,
                Padding = new Thickness(5.0),
                Background = Brushes.White,
            };
            return new ControlWrapper(border, GraphNodeDirection.DownRight);
        }
    }
}
