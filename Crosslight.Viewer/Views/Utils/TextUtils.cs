using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Viewer.Views.Utils
{
    public static class TextUtils
    {
        public static Size GetTextSize(string text)
        {
            var textBlock = new TextBlock { Text = text, TextWrapping = TextWrapping.Wrap };
            // auto sized
            textBlock.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            textBlock.Arrange(new Rect(textBlock.DesiredSize));
            return textBlock.Bounds.Size;
        }
    }
}
