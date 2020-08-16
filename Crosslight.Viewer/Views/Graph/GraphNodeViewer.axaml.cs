using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Crosslight.Viewer.Views.Graph
{
    public class GraphNodeViewer : UserControl
    {
        public GraphNodeViewer()
        {
            this.InitializeComponent();

            AffectsRender<Border>(
                ChildBorderBrushProperty,
                ChildBorderThicknessProperty,
                ChildBorderCornerRadiusProperty
            );
            AffectsMeasure<Border>(ChildBorderThicknessProperty);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        /// <summary>
        /// Defines the <see cref="BorderBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ChildBorderBrushProperty =
            AvaloniaProperty.Register<Border, IBrush>(nameof(ChildBorderBrush));

        /// <summary>
        /// Defines the <see cref="BorderThickness"/> property.
        /// </summary>
        public static readonly StyledProperty<Thickness> ChildBorderThicknessProperty =
            AvaloniaProperty.Register<Border, Thickness>(nameof(ChildBorderThickness));

        /// <summary>
        /// Defines the <see cref="CornerRadius"/> property.
        /// </summary>
        public static readonly StyledProperty<CornerRadius> ChildBorderCornerRadiusProperty =
            AvaloniaProperty.Register<GraphNodeViewer, CornerRadius>(nameof(ChildBorderCornerRadius));

        /// <summary>
        /// Defines the <see cref="Thickness"/> property.
        /// </summary>
        public static readonly StyledProperty<Thickness> ChildPaddingProperty =
            AvaloniaProperty.Register<GraphNodeViewer, Thickness>(nameof(ChildPadding));

        /// <summary>
        /// Gets or sets a brush with which to paint the border.
        /// </summary>
        public IBrush ChildBorderBrush
        {
            get { return GetValue(ChildBorderBrushProperty); }
            set { SetValue(ChildBorderBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the border.
        /// </summary>
        public Thickness ChildBorderThickness
        {
            get { return GetValue(ChildBorderThicknessProperty); }
            set { SetValue(ChildBorderThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the radius of the border rounded corners.
        /// </summary>
        public CornerRadius ChildBorderCornerRadius
        {
            get { return GetValue(ChildBorderCornerRadiusProperty); }
            set { SetValue(ChildBorderCornerRadiusProperty, value); }
        }

        /// <summary>
        /// Gets or sets the radius of the border rounded corners.
        /// </summary>
        public Thickness ChildPadding
        {
            get { return GetValue(ChildPaddingProperty); }
            set { SetValue(ChildPaddingProperty, value); }
        }
    }
}
