using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using Crosslight.Viewer.ViewModels.Graph;
using Crosslight.Viewer.Views.Utils;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Disposables;

namespace Crosslight.Viewer.Views.Graph
{
    public class GraphNodeViewer : ReactiveUserControl<NodeViewModel>
    {
        private TextBlock NodeText => this.FindControl<TextBlock>("nodeText");
        private Button NodeButton => this.FindControl<Button>("nodeButton");
        private Image NodeIcon => this.FindControl<Image>("nodeIcon");

        private static readonly NodeTypeToIconConverter conNtoI = new NodeTypeToIconConverter();

        public GraphNodeViewer()
        {
            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, x => x.Data, x => x.NodeText.Text)
                    .DisposeWith(disp);
                
                NodeButton.Bind(Button.BorderBrushProperty, this.GetObservable(ChildBorderBrushProperty)).DisposeWith(disp);
                NodeButton.Bind(Button.BorderThicknessProperty, this.GetObservable(ChildBorderThicknessProperty)).DisposeWith(disp);
                NodeButton.Bind(Button.PaddingProperty, this.GetObservable(ChildPaddingProperty)).DisposeWith(disp);
                NodeButton.Bind(Button.BackgroundProperty, this.GetObservable(ChildBackgroundProperty)).DisposeWith(disp);

                this.OneWayBind(ViewModel, x => x.Type, x => x.NodeIcon.Source, null, conNtoI).DisposeWith(disp);
                this.OneWayBind(ViewModel, x => x.SetStartNode, x => x.NodeButton.Command).DisposeWith(disp);
            });
            this.InitializeComponent();

            AffectsRender<Border>(
                ChildBackgroundProperty,
                ChildBorderBrushProperty,
                ChildBorderThicknessProperty
            );
            AffectsMeasure<Border>(ChildBorderThicknessProperty);
        }

        public Drawing GetDrawing(string name)
        {
            try
            {
                return Resources[name] as Drawing;
            }
            catch (Exception)
            {
                return null;
            }
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
            //if (Content is ILayoutable layoutable)
            //    return LayoutHelper.MeasureChild(layoutable, availableSize, Padding, BorderThickness);
            return base.MeasureOverride(availableSize);
        }
    }
}
