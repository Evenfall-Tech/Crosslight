//////////////////////////////////////////////
// Apache 2.0  - 2016-2020
// Author : Derek Tremblay (derektremblay666@gmail.com)
//////////////////////////////////////////////

using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;

namespace Crosslight.Common.UI.Controls.HexEditorControl
{
    /// <summary>
    /// Light Label like control
    /// </summary>
    internal class FastTextLine : Control
    {
        private readonly HexEditor _parent;

        #region Constructor

        static FastTextLine()
        {
            AffectsRender<FastTextLine>(BackgroundProperty, TextProperty);
            AffectsMeasure<FastTextLine>(TextProperty, RenderPointProperty);
        }

        public FastTextLine(HexEditor parent)
        {
            //Parent hexeditor
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));

            //Default properties
            DataContext = this;
        }

        #endregion Contructor

        #region Base properties

        /// <summary>
        /// Definie the foreground
        /// </summary>
        public static readonly StyledProperty<IBrush> ForegroundProperty =
            TextBlock.ForegroundProperty.AddOwner<FastTextLine>();

        public IBrush Foreground
        {
            get => (IBrush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        public static readonly StyledProperty<IBrush> BackgroundProperty =
            TextBlock.BackgroundProperty.AddOwner<FastTextLine>();

        /// <summary>
        /// Defines the background
        /// </summary>
        public Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<FastTextLine, string>(nameof(Text), defaultValue: string.Empty);

        /// <summary>
        /// Text to be displayed representation of Byte
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly StyledProperty<FontWeight> FontWeightProperty =
            TextBlock.FontWeightProperty.AddOwner<FastTextLine>();

        /// <summary>
        /// The FontWeight property specifies the weight of the font.
        /// </summary>
        public FontWeight FontWeight
        {
            get => (FontWeight)GetValue(FontWeightProperty);
            set => SetValue(FontWeightProperty, value);
        }

        #endregion Base properties

        #region Properties

        /// <summary>
        /// Get or set if the width are auto or fixed
        /// </summary>
        public bool AutoWidth { get; set; } = true;

        /// <summary>
        /// Get or set the render point
        /// </summary>
        public Point RenderPoint
        {
            get => (Point)GetValue(RenderPointProperty);
            set => SetValue(RenderPointProperty, value);
        }

        public static readonly StyledProperty<Point> RenderPointProperty =
            AvaloniaProperty.Register<FastTextLine, Point>(nameof(RenderPoint), defaultValue: new Point(0, 0));

        #endregion

        /// <summary>
        /// Render the control
        /// </summary>
        public override void Render(DrawingContext dc)
        {
            //Draw background
            if (Background != null)
                dc.DrawRectangle(Background, null, new Rect(0, 0, Bounds.Width, Bounds.Height));

            //Draw text
            var formatedText = new FormattedText(
                Text,
                new Typeface(_parent.FontFamily, _parent.FontStyle, FontWeight),
                _parent.FontSize,
                TextAlignment.Left,
                TextWrapping.Wrap,
                Bounds.Size);

            dc.DrawText(Foreground, new Point(RenderPoint.X, RenderPoint.Y), formatedText);

            if (AutoWidth)
                Width = formatedText.Bounds.Width + RenderPoint.X;
        }
    }
}
