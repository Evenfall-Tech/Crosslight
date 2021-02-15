//////////////////////////////////////////////
// Fork 2017-2020 : Derek Tremblay (derektremblay666@gmail.com) 
// Part of Wpf HexEditor control : https://github.com/abbaye/WPFHexEditorControl
// Reference : https://www.codeproject.com/Tips/431000/Caret-for-WPF-User-Controls
// Reference license : The Code Project Open License (CPOL) 1.02
// Contributor : emes30
//////////////////////////////////////////////

using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Core
{
    /// <summary>
    /// This class represent a visual caret on editor
    /// </summary>
    public sealed class Caret : Control, INotifyPropertyChanged
    {
        #region Global class variables
        private Timer _timer;
        private Point _position;
        private readonly Pen _pen = new Pen(Brushes.Black, 1);
        private readonly Brush _brush = new SolidColorBrush(Colors.Black);
        private int _blinkPeriod = 500;
        private double _caretHeight = 18;
        private double _caretWidth = 9;
        private bool _hide;
        private CaretMode _caretMode = CaretMode.Overwrite;
        #endregion

        #region Constructor
        static Caret()
        {
            AffectsRender<Caret>(VisibleProperty);
            ClipToBoundsProperty.OverrideDefaultValue<Caret>(true);

            LayoutTransformProperty.Changed
                .AddClassHandler<Caret>((x, e) => x.OnLayoutTransformChanged(e as AvaloniaPropertyChangedEventArgs<ITransform>));
        }

        public Caret()
        {
            if (Design.IsDesignMode) return;

            // _pen.Freeze();
            _brush.Opacity = .5;
            IsHitTestVisible = false;
            InitializeTimer();
            Hide();
        }

        public Caret(Brush brush)
        {
            if (Design.IsDesignMode) return;

            _pen.Brush = brush;
            // _pen.Freeze();
            _brush.Opacity = .5;
            IsHitTestVisible = false;
            InitializeTimer();
            Hide();
        }
        #endregion

        #region Properties

        private static readonly StyledProperty<bool> VisibleProperty =
            AvaloniaProperty.Register<Caret, bool>(nameof(Visible), defaultValue: false);

        public static readonly StyledProperty<ITransform> LayoutTransformProperty =
            AvaloniaProperty.Register<LayoutTransformControl, ITransform>(nameof(LayoutTransform));

        public static readonly DirectProperty<Caret, double> CaretHeightProperty =
            AvaloniaProperty.RegisterDirect<Caret, double>(nameof(CaretHeight), c => c.CaretHeight, (c, v) => c.CaretHeight = v);

        public static readonly DirectProperty<Caret, double> CaretWidthProperty =
            AvaloniaProperty.RegisterDirect<Caret, double>(nameof(CaretWidth), c => c.CaretWidth, (c, v) => c.CaretWidth = v);

        public static readonly DirectProperty<Caret, Point> PositionProperty =
            AvaloniaProperty.RegisterDirect<Caret, Point>(nameof(Position), c => c.Position);

        public static readonly DirectProperty<Caret, double> LeftProperty =
            AvaloniaProperty.RegisterDirect<Caret, double>(nameof(Left), c => c.Left, (c, v) => c.Left = v);

        public static readonly DirectProperty<Caret, double> TopProperty =
            AvaloniaProperty.RegisterDirect<Caret, double>(nameof(Top), c => c.Top, (c, v) => c.Top = v);

        public static readonly DirectProperty<Caret, bool> IsEnableProperty =
            AvaloniaProperty.RegisterDirect<Caret, bool>(nameof(IsEnable), c => c.IsEnable);

        public static readonly DirectProperty<Caret, CaretMode> CaretModeProperty =
            AvaloniaProperty.RegisterDirect<Caret, CaretMode>(nameof(CaretMode), c => c.CaretMode, (c, v) => c.CaretMode = v);

        public static readonly DirectProperty<Caret, int> BlinkPeriodProperty =
            AvaloniaProperty.RegisterDirect<Caret, int>(nameof(BlinkPeriod), c => c.BlinkPeriod, (c, v) => c.BlinkPeriod = v);

        /// <summary>
        /// Get is caret is running
        /// </summary>
        public bool IsEnable => _timer != null;

        /// <summary>
        /// Propertie used when caret is blinking
        /// </summary>
        private bool Visible
        {
            get => (bool)GetValue(VisibleProperty);
            set => SetValue(VisibleProperty, value);
        }

        /// <summary>
        /// Gets or sets a graphics transformation that should apply to this element when layout is performed.
        /// </summary>
        public ITransform LayoutTransform
        {
            get { return GetValue(LayoutTransformProperty); }
            set { SetValue(LayoutTransformProperty, value); }
        }

        /// <summary>
        /// Height of the caret
        /// </summary>
        public double CaretHeight
        {
            get => _caretHeight;
            set
            {
                if (_caretHeight == value) return;
                if (value < 0) value = 0;
                this.SetAndRaise(CaretHeightProperty, ref _caretHeight, value);

                //InitializeTimer();
            }
        }

        /// <summary>
        /// Width of the caret
        /// </summary>
        public double CaretWidth
        {
            get => _caretWidth;
            set
            {
                if (_caretWidth == value) return;
                if (value < 0) value = 0;
                this.SetAndRaise(CaretWidthProperty, ref _caretWidth, value);
            }
        }

        /// <summary>
        /// Get the relative position of the caret
        /// </summary>
        public Point Position => _position;

        /// <summary>
        /// Left position of the caret
        /// </summary>
        public double Left
        {
            get => _position.X;
            private set
            {
                if (_position.X == value) return;
                var oldPosition = _position;
                _position = _position.WithX(Math.Floor(value));
                if (Visible) Visible = false;

                this.RaisePropertyChanged(PositionProperty, oldPosition, _position);
                this.RaisePropertyChanged(LeftProperty, oldPosition.X, _position.X);
            }
        }

        /// <summary>
        /// Top position of the caret
        /// </summary>
        public double Top
        {
            get => _position.Y;
            private set
            {
                if (_position.Y == value) return;
                var oldPosition = _position;
                _position = _position.WithY(Math.Floor(value));
                if (Visible) Visible = false;

                this.RaisePropertyChanged(PositionProperty, oldPosition, _position);
                this.RaisePropertyChanged(TopProperty, oldPosition.Y, _position.Y);
            }
        }

        /// <summary>
        /// Properties return true if caret is visible
        /// </summary>
        public bool IsVisibleCaret => Left >= 0 && Top > 0 && _hide == false;

        /// <summary>
        /// Blick period in millisecond
        /// </summary>
        public int BlinkPeriod
        {
            get => _blinkPeriod;
            set
            {
                var oldValue = _blinkPeriod;
                _blinkPeriod = value;
                InitializeTimer();

                this.RaisePropertyChanged(BlinkPeriodProperty, oldValue, value);
            }
        }

        /// <summary>
        /// Caret display mode. Line for Insert, Block for Overwrite
        /// </summary>
        public CaretMode CaretMode
        {
            get => _caretMode;
            set
            {
                this.SetAndRaise(CaretModeProperty, ref _caretMode, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Hide the caret
        /// </summary>
        public void Hide() => _hide = true;

        /// <summary>
        /// Method delegate for blink the caret
        /// </summary>
        private void BlinkCaret(object state)
        {
            try
            {
                Dispatcher.UIThread?.InvokeAsync(() =>
                {
                    Visible = !Visible && !_hide;
                }).Wait();
            }
            catch { }
        }

        /// <summary>
        /// Initialise the timer
        /// </summary>
        private void InitializeTimer() => _timer = new Timer(BlinkCaret, null, 0, BlinkPeriod);

        /// <summary>
        /// Move the caret over the position defined by point parameter
        /// </summary>
        public void MoveCaret(Point point) => MoveCaret(point.X, point.Y);

        /// <summary>
        /// Move the caret over the position defined by point parameter
        /// </summary>
        public void MoveCaret(double x, double y)
        {
            _hide = false;
            Left = x;
            Top = y;
        }


        /// <summary>
        /// Start the caret
        /// </summary>
        public void Start()
        {
            InitializeTimer();

            _hide = false;

            this.RaisePropertyChanged(IsEnableProperty, false, true);
        }

        /// <summary>
        /// Stop the carret
        /// </summary>
        public void Stop()
        {
            Hide();
            _timer = null;

            this.RaisePropertyChanged(IsEnableProperty, true, false);
        }

        /// <summary>
        /// Render the caret
        /// </summary>
        public override void Render(DrawingContext dc)
        {
            if (!Visible) return;

            switch (_caretMode)
            {
                case CaretMode.Insert:
                    if (LayoutTransform != null)
                        dc.DrawLine(_pen, _position.Transform(LayoutTransform.Value), new Point(Left, _position.Y + CaretHeight).Transform(LayoutTransform.Value));
                    else
                        dc.DrawLine(_pen, _position, new Point(Left, _position.Y + CaretHeight));
                    break;
                case CaretMode.Overwrite:
                    if (LayoutTransform != null)
                        dc.DrawRectangle(_brush, _pen, new Rect(Left, _position.Y, _caretWidth, CaretHeight).TransformToAABB(LayoutTransform.Value));
                    else
                        dc.DrawRectangle(_brush, _pen, new Rect(Left, _position.Y, _caretWidth, CaretHeight));
                    break;
            }
        }
        #endregion

        /// <summary>
        /// Rounds the non-offset elements of a Matrix to avoid issues due to floating point imprecision.
        /// </summary>
        /// <param name="matrix">Matrix to round.</param>
        /// <param name="decimals">Number of decimal places to round to.</param>
        /// <returns>Rounded Matrix.</returns>
        private static Matrix RoundMatrix(Matrix matrix, int decimals)
        {
            return new Matrix(
                Math.Round(matrix.M11, decimals),
                Math.Round(matrix.M12, decimals),
                Math.Round(matrix.M21, decimals),
                Math.Round(matrix.M22, decimals),
                matrix.M31,
                matrix.M32);
        }

        private void OnLayoutTransformChanged(AvaloniaPropertyChangedEventArgs<ITransform> e)
        {
            InvalidateVisual();
        }
    }
}
