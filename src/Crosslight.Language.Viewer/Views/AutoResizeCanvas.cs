using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Layout;
using Avalonia.VisualTree;
using System;
using System.Linq;

namespace Crosslight.Language.Viewer.Views
{
    /// <summary>
    /// A panel that displays child controls at arbitrary locations.
    /// Resizes on children collection modification.
    /// Resizing from https://stackoverflow.com/a/21532718/2832341.
    /// Code from https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Controls/Canvas.cs.
    /// </summary>
    /// <remarks>
    /// Unlike other <see cref="Panel"/> implementations, the <see cref="AutoResizeCanvas"/> doesn't lay out
    /// its children in any particular layout. Instead, the positioning of each child control is
    /// defined by the <code>Canvas.Left</code>, <code>Canvas.Top</code>, <code>Canvas.Right</code>
    /// and <code>Canvas.Bottom</code> attached properties.
    /// </remarks>
    public class AutoResizeCanvas : Canvas
    {
        /// <summary>
        /// Measures the control.
        /// </summary>
        /// <param name="availableSize">The available size.</param>
        /// <returns>The desired size of the control.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            availableSize = new Size(double.MaxValue, double.MaxValue);
            double requestedWidth = MinWidth;
            double requestedHeight = MinHeight;
            foreach (var child in Children)
            {
                if (child != null)
                {
                    child.Measure(availableSize);
                    GetRequestedBounds(child, out Rect bounds, out Rect margin);

                    requestedWidth = Math.Max(requestedWidth, margin.Right);
                    requestedHeight = Math.Max(requestedHeight, margin.Bottom);
                }
            }
            return new Size(requestedWidth, requestedHeight);
        }

        private void GetRequestedBounds(
                            ILayoutable el,
                            out Rect bounds, out Rect marginBounds
                            )
        {
            // TODO: implement GetRight and GetBottom
            double left = 0, top = 0;
            Thickness margin = new Thickness();
            AvaloniaObject content = el as AvaloniaObject;
            if (el is IContentPresenter presenter)
            {
                content = presenter.GetVisualChildren().OfType<AvaloniaObject>().FirstOrDefault();
            }
            if (content != null)
            {
                left = GetLeft(content);
                top = GetTop(content);
                if (content is ILayoutable layoutable)
                {
                    margin = layoutable.Margin;
                }
            }
            if (double.IsNaN(left)) left = 0;
            if (double.IsNaN(top)) top = 0;
            Size size = el.DesiredSize;
            bounds = new Rect(left + margin.Left, top + margin.Top, size.Width, size.Height);
            marginBounds = new Rect(left, top, size.Width + margin.Left + margin.Right, size.Height + margin.Top + margin.Bottom);
        }

        /// <summary>
        /// Arranges the control's children.
        /// </summary>
        /// <param name="finalSize">The size allocated to the control.</param>
        /// <returns>The space taken.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            double requestedWidth = MinWidth;
            double requestedHeight = MinHeight;
            foreach (var child in Children)
            {
                if (child != null)
                {
                    GetRequestedBounds(child, out Rect bounds, out Rect marginBounds);

                    requestedWidth = Math.Max(marginBounds.Right, requestedWidth);
                    requestedHeight = Math.Max(marginBounds.Bottom, requestedHeight);
                    child.Arrange(bounds);
                }
            }
            return new Size(requestedWidth, requestedHeight);
        }
    }
}
