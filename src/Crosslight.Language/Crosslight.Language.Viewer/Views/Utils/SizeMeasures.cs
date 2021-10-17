using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace Crosslight.Language.Viewer.Views.Utils
{
    public static class SizeMeasures
    {
        public static Size GetTextSize(string text)
        {
            var textBlock = new TextBlock { Text = text, TextWrapping = TextWrapping.Wrap };
            // auto sized
            return GetMinControlSize(textBlock);
        }

        public static Size GetMinControlSize(Layoutable control)
        {
            control.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            control.Arrange(new Rect(control.DesiredSize));
            return control.Bounds.Size;
        }
    }
}
