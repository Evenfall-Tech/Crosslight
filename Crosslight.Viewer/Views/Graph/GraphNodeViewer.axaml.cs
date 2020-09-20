using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using Crosslight.Viewer.ViewModels.Graph;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Crosslight.Viewer.Views.Graph
{
    public class GraphNodeViewer : ReactiveUserControl<NodeViewModel>
    {
        private TextBlock NodeText => this.FindControl<TextBlock>("nodeText");
        private Border NodeBorder => this.FindControl<Border>("nodeBorder");
        public GraphNodeViewer()
        {
            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, x => x.Data, x => x.NodeText.Text)
                    .DisposeWith(disp);
                NodeBorder.Bind(Border.CornerRadiusProperty, this.GetObservable(ChildBorderCornerRadiusProperty)).DisposeWith(disp);
                NodeBorder.Bind(Border.BorderBrushProperty, this.GetObservable(ChildBorderBrushProperty)).DisposeWith(disp);
                NodeBorder.Bind(Border.BorderThicknessProperty, this.GetObservable(ChildBorderThicknessProperty)).DisposeWith(disp);
                NodeBorder.Bind(Border.PaddingProperty, this.GetObservable(ChildPaddingProperty)).DisposeWith(disp);
                NodeBorder.Bind(Border.BackgroundProperty, this.GetObservable(ChildBackgroundProperty)).DisposeWith(disp);
            });
            this.InitializeComponent();

            AffectsRender<Border>(
                ChildBackgroundProperty,
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
        /// Defines the <see cref="ChildBackground"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ChildBackgroundProperty =
            AvaloniaProperty.Register<Border, IBrush>(nameof(ChildBackground));

        /// <summary>
        /// Defines the <see cref="ChildBorderBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ChildBorderBrushProperty =
            AvaloniaProperty.Register<Border, IBrush>(nameof(ChildBorderBrush));

        /// <summary>
        /// Defines the <see cref="ChildBorderThickness"/> property.
        /// </summary>
        public static readonly StyledProperty<Thickness> ChildBorderThicknessProperty =
            AvaloniaProperty.Register<Border, Thickness>(nameof(ChildBorderThickness));

        /// <summary>
        /// Defines the <see cref="ChildBorderCornerRadius"/> property.
        /// </summary>
        public static readonly StyledProperty<CornerRadius> ChildBorderCornerRadiusProperty =
            AvaloniaProperty.Register<GraphNodeViewer, CornerRadius>(nameof(ChildBorderCornerRadius));

        /// <summary>
        /// Defines the <see cref="ChildPadding"/> property.
        /// </summary>
        public static readonly StyledProperty<Thickness> ChildPaddingProperty =
            AvaloniaProperty.Register<GraphNodeViewer, Thickness>(nameof(ChildPadding));

        /// <summary>
        /// Gets or sets the brush used to draw the control's child's background.
        /// </summary>
        public IBrush ChildBackground
        {
            get { return GetValue(ChildBackgroundProperty); }
            set { SetValue(ChildBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets a brush with which to paint the child border.
        /// </summary>
        public IBrush ChildBorderBrush
        {
            get { return GetValue(ChildBorderBrushProperty); }
            set { SetValue(ChildBorderBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the child border.
        /// </summary>
        public Thickness ChildBorderThickness
        {
            get { return GetValue(ChildBorderThicknessProperty); }
            set { SetValue(ChildBorderThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the radius of the child border rounded corners.
        /// </summary>
        public CornerRadius ChildBorderCornerRadius
        {
            get { return GetValue(ChildBorderCornerRadiusProperty); }
            set { SetValue(ChildBorderCornerRadiusProperty, value); }
        }

        /// <summary>
        /// Gets or sets the padding placed between the border of the child control and its content.
        /// </summary>
        public Thickness ChildPadding
        {
            get { return GetValue(ChildPaddingProperty); }
            set { SetValue(ChildPaddingProperty, value); }
        }

        /// <summary>
        /// Measures the control.
        /// </summary>
        /// <param name="availableSize">The available size.</param>
        /// <returns>The desired size of the control.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (Content is ILayoutable layoutable)
                return LayoutHelper.MeasureChild(layoutable, availableSize, Padding, BorderThickness);
            return base.MeasureOverride(availableSize);
        }
    }
}
