//////////////////////////////////////////////
// Apache 2.0  - 2016-2021
// Base author : Derek Tremblay (derektremblay666@gmail.com)
// Contributor : emes30
// Contributor : ehsan69h
// Contributor : Janus Tida
//////////////////////////////////////////////

using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;
using Crosslight.Common.UI.Controls.HexEditorControl.Core;
using Crosslight.Common.UI.Controls.HexEditorControl.Core.EventArguments;
using Crosslight.Common.UI.Controls.HexEditorControl.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Crosslight.Common.UI.Controls.HexEditorControl
{
    /// <summary>
    /// Base class for bytecontrol
    /// </summary>
    abstract class BaseByte : Control, IByteControl
    {
        #region Global class variables
        protected readonly HexEditor _parent;
        private bool _isSelected;
        private ByteAction _action = ByteAction.Nothing;
        private IByte _byte;
        private bool _isHighLight;
        #endregion global class variables

        #region Events

        public event EventHandler<ByteEventArgs> ByteModified;
        public event EventHandler MouseSelection;
        public event EventHandler Click;
        public event EventHandler RightClick;
        public event EventHandler DoubleClick;
        public event EventHandler<ByteEventArgs> MoveNext;
        public event EventHandler<ByteEventArgs> MovePrevious;
        public event EventHandler<ByteEventArgs> MoveRight;
        public event EventHandler<ByteEventArgs> MoveLeft;
        public event EventHandler<ByteEventArgs> MoveUp;
        public event EventHandler<ByteEventArgs> MoveDown;
        public event EventHandler<ByteEventArgs> MovePageDown;
        public event EventHandler<ByteEventArgs> MovePageUp;
        public event EventHandler ByteDeleted;
        public event EventHandler EscapeKey;
        public event EventHandler CtrlzKey;
        public event EventHandler CtrlvKey;
        public event EventHandler CtrlcKey;
        public event EventHandler CtrlaKey;
        public event EventHandler CtrlyKey;

        #endregion Events

        #region Constructor

        static BaseByte()
        {
            AffectsRender<BaseByte>(BackgroundProperty, TextProperty);
            AffectsMeasure<BaseByte>(TextProperty);

            ToolTip.IsOpenProperty.Changed.AddClassHandler<BaseByte>((x, e) => OnToolTipOpening(x, e));
        }

        protected BaseByte(HexEditor parent)
        {
            //Parent hexeditor
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));

            #region Binding tooltip

            LoadDictionary("avares://Crosslight.Common.UI/Assets/Controls/HexEditorControl/Dictionary/ToolTipDictionary.xaml");
            var txtBinding = new Binding()
            {
                Source = this.FindResource("ByteToolTip"),
                Mode = BindingMode.OneWay
            };

            // Load ressources dictionnary
            void LoadDictionary(string url)
            {
                var ttRes = new ResourceInclude { Source = new Uri(url, UriKind.Relative) };
                Resources.MergedDictionaries.Add(ttRes);
            }

            this.Bind(ToolTip.TipProperty, txtBinding);

            #endregion

            //Default properties
            DataContext = this;
            Focusable = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Position in file
        /// </summary>
        public long BytePositionInStream { get; set; } = -1L;

        /// <summary>
        /// Used for selection coloring
        /// </summary>
        public bool FirstSelected { protected get; set; }

        /// <summary>
        /// Used to prevent ByteModified event occurc when we dont want! 
        /// </summary>
        public bool InternalChange { get; set; }

        /// <summary>
        /// Get or set if control as in read only mode
        /// </summary>
        public bool ReadOnlyMode { protected get; set; }

        /// <summary>
        /// Get or set the description to shown in tooltip
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Return true if mouse is over... (Used with traverse methods via IByteControl)
        /// </summary>
        public bool IsMouseOverMe { get; internal set; }

        /// <summary>
        /// Get or Set if control as selected
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value == _isSelected) return;

                _isSelected = value;
                UpdateVisual();
            }
        }

        /// <summary>
        /// Get of Set if control as marked as highlighted
        /// </summary>   
        public bool IsHighLight
        {
            get => _isHighLight;
            set
            {
                if (value == _isHighLight) return;

                _isHighLight = value;
                UpdateVisual();
            }
        }

        /// <summary>
        /// Byte used for this instance
        /// </summary>
        public IByte Byte
        {
            get => _byte;
            set
            {
                _byte = value;

                UpdateTextRenderFromByte();

                if (value != null)
                    _byte.del_ByteOnChange += OnByteChange;
            }
        }

        internal void OnByteChange(List<byte> bytes, int index)
        {
            //if (Action != ByteAction.Nothing && InternalChange == false)
            if (InternalChange == false)
                ByteModified?.Invoke(this, new ByteEventArgs() { Index = index });

            UpdateTextRenderFromByte();
        }

        /// <summary>
        /// Action with this byte
        /// </summary>
        public ByteAction Action
        {
            get => _action;
            set
            {
                _action = value != ByteAction.All ? value : ByteAction.Nothing;

                UpdateVisual();
            }
        }

        protected FormattedText TextFormatted { get; private set; }

        #endregion

        #region Private base properties

        /// <summary>
        /// Definie the foreground
        /// </summary>
        private static readonly StyledProperty<IBrush> ForegroundProperty =
            TextBlock.ForegroundProperty.AddOwner<BaseByte>();

        protected Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        public static readonly StyledProperty<IBrush> BackgroundProperty =
            TextBlock.BackgroundProperty.AddOwner<BaseByte>();

        /// <summary>
        /// Defines the background
        /// </summary>
        protected IBrush Background
        {
            get => GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        private static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<BaseByte, string>(nameof(Text));

        /// <summary>
        /// Text to be displayed representation of Byte
        /// </summary>
        protected string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static readonly StyledProperty<FontWeight> FontWeightProperty =
            TextBlock.FontWeightProperty.AddOwner<BaseByte>();

        /// <summary>
        /// The FontWeight property specifies the weight of the font.
        /// </summary>
        protected FontWeight FontWeight
        {
            get => (FontWeight)GetValue(FontWeightProperty);
            set => SetValue(FontWeightProperty, value);
        }

        #endregion Base properties

        #region Methods

        /// <summary>
        /// Update Background,foreground and font property
        /// </summary>
        public virtual void UpdateVisual()
        {
            if (IsSelected)
            {
                FontWeight = _parent.FontWeight;
                Foreground = _parent.ForegroundContrast;

                Background = FirstSelected
                    ? _parent.SelectionFirstColor
                    : _parent.SelectionSecondColor;
            }
            else if (IsHighLight)
            {
                FontWeight = _parent.FontWeight;
                Foreground = _parent.Foreground;
                Background = _parent.HighLightColor;
            }
            else if (Action != ByteAction.Nothing)
            {
                FontWeight = FontWeight.Bold;
                Foreground = _parent.Foreground;

                switch (Action)
                {
                    case ByteAction.Modified:
                        Background = _parent.ByteModifiedColor;
                        break;
                    case ByteAction.Deleted:
                        Background = _parent.ByteDeletedColor;
                        break;
                    case ByteAction.Added:
                        Background = _parent.ByteAddedColor;
                        break;
                }
            }
            else //Aoply a CustomBackgroundBlock over byte if needed
            {
                var cbb = _parent.GetCustomBackgroundBlock(BytePositionInStream);

                Description = cbb != null ? cbb.Description : "";

                Background = cbb != null ? cbb.Color : Brushes.Transparent;

                Foreground = _parent.GetColumnNumber(BytePositionInStream) % 2 == 0
                    ? _parent.Foreground
                    : _parent.ForegroundSecondColor;

                FontWeight = _parent.FontWeight;
            }

            UpdateAutoHighLiteSelectionByteVisual();

            InvalidateVisual();
        }

        /// <summary>
        /// Auto highlite SelectionByte
        /// </summary>
        protected void UpdateAutoHighLiteSelectionByteVisual()
        {
            if (_parent.AllowAutoHighLightSelectionByte && _parent.SelectionByte != null &&
                Byte != null && Byte.IsEqual(new byte[] { _parent.SelectionByte.Value }) && !IsSelected)
                Background = _parent.AutoHighLiteSelectionByteBrush;
        }

        /// <summary>
        /// Update the render of text derived bytecontrol from byte property
        /// </summary>
        public abstract void UpdateTextRenderFromByte();

        /// <summary>
        /// Clear control
        /// </summary>
        public virtual void Clear()
        {
            InternalChange = true;
            Byte = null;
            BytePositionInStream = -1;
            Action = ByteAction.Nothing;
            IsSelected = false;
            Description = string.Empty;
            InternalChange = false;
        }

        #endregion

        #region Events delegate

        /// <summary>
        /// Render the control
        /// </summary>
        public override void Render(DrawingContext dc)
        {
            //Draw background
            if (Background != null)
                //HACK: We don't have support for RenderSize for now
                dc.DrawRectangle(Background, null, new Rect(0, 0, Bounds.Width, Bounds.Height));

            //Draw text
            var typeface = new Typeface(_parent.FontFamily, _parent.FontStyle, FontWeight);

            var formattedText = new FormattedText(Text, typeface, _parent.FontSize, TextAlignment.Left, TextWrapping.Wrap, Size.Infinity);

            dc.DrawText(Foreground, new Point(2, 2), formattedText);

            //Update properties
            TextFormatted = formattedText;
        }

        protected override void OnPointerEnter(PointerEventArgs e)
        {
            if (Byte != null && !IsSelected && !IsHighLight &&
                Action != ByteAction.Modified &&
                Action != ByteAction.Deleted &&
                Action != ByteAction.Added)
                Background = _parent.MouseOverColor;

            UpdateAutoHighLiteSelectionByteVisual();

            if (e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonPressed)
                MouseSelection?.Invoke(this, e);

            IsMouseOverMe = true;

            base.OnPointerEnter(e);
        }

        protected override void OnPointerLeave(PointerEventArgs e)
        {
            var cbb = _parent.GetCustomBackgroundBlock(BytePositionInStream);

            if (Byte != null && !IsSelected && !IsHighLight &&
                Action != ByteAction.Modified &&
                Action != ByteAction.Deleted &&
                Action != ByteAction.Added)
                Background = Brushes.Transparent;

            if (cbb != null && !IsSelected && !IsHighLight &&
                Action != ByteAction.Modified &&
                Action != ByteAction.Deleted &&
                Action != ByteAction.Added)
                Background = cbb.Color;

            IsMouseOverMe = false;

            UpdateAutoHighLiteSelectionByteVisual();

            base.OnPointerLeave(e);
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonPressed)
            {
                if (!IsFocused)
                    Focus();

                switch (e.ClickCount)
                {
                    case 1:
                        Click?.Invoke(this, e);
                        break;
                    case 2:
                        DoubleClick?.Invoke(this, e);
                        break;
                }
            }

            if (e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.RightButtonPressed)
                RightClick?.Invoke(this, e);

            base.OnPointerPressed(e);
        }

        static void OnToolTipOpening(IAvaloniaObject d, AvaloniaPropertyChangedEventArgs e)
        {
            if (!(d is BaseByte ctrl)) return;
            // Process only for showing, not hiding.
            if (!(bool)e.NewValue) return;
            if (ctrl.Byte == null || !ctrl._parent.ShowByteToolTip)
            {
                e.Handled = true;
            }
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            _parent.HideCaret();
            base.OnLostFocus(e);
        }

        protected void OnMoveNext(ByteEventArgs e) => MoveNext?.Invoke(this, e);

        protected bool KeyValidation(KeyEventArgs e)
        {
            #region Key validation and launch event if needed

            if (KeyValidator.IsUpKey(e.Key))
            {
                e.Handled = true;
                MoveUp?.Invoke(this, new ByteEventArgs(BytePositionInStream) { Inner = e });

                return true;
            }
            if (KeyValidator.IsDownKey(e.Key))
            {
                e.Handled = true;
                MoveDown?.Invoke(this, new ByteEventArgs(BytePositionInStream) { Inner = e });

                return true;
            }
            if (KeyValidator.IsLeftKey(e.Key))
            {
                e.Handled = true;
                MoveLeft?.Invoke(this, new ByteEventArgs(BytePositionInStream) { Inner = e });

                return true;
            }
            if (KeyValidator.IsRightKey(e.Key))
            {
                e.Handled = true;
                MoveRight?.Invoke(this, new ByteEventArgs(BytePositionInStream) { Inner = e });

                return true;
            }
            if (KeyValidator.IsPageDownKey(e.Key))
            {
                e.Handled = true;
                MovePageDown?.Invoke(this, new ByteEventArgs(BytePositionInStream) { Inner = e });

                return true;
            }
            if (KeyValidator.IsPageUpKey(e.Key))
            {
                e.Handled = true;
                MovePageUp?.Invoke(this, new ByteEventArgs(BytePositionInStream) { Inner = e });

                return true;
            }
            if (KeyValidator.IsDeleteKey(e.Key))
            {
                if (!ReadOnlyMode)
                {
                    e.Handled = true;
                    ByteDeleted?.Invoke(this, new EventArgs());

                    return true;
                }
            }
            else if (KeyValidator.IsBackspaceKey(e.Key))
            {
                e.Handled = true;
                ByteDeleted?.Invoke(this, new EventArgs());

                MovePrevious?.Invoke(this, new ByteEventArgs(BytePositionInStream) { Inner = e });

                return true;
            }
            else if (KeyValidator.IsEscapeKey(e.Key))
            {
                e.Handled = true;
                EscapeKey?.Invoke(this, new EventArgs());
                return true;
            }
            else if (KeyValidator.IsCtrlZKey(new KeyGesture(e.Key, e.KeyModifiers)))
            {
                e.Handled = true;
                CtrlzKey?.Invoke(this, new EventArgs());
                return true;
            }
            else if (KeyValidator.IsCtrlYKey(new KeyGesture(e.Key, e.KeyModifiers)))
            {
                e.Handled = true;
                CtrlyKey?.Invoke(this, new EventArgs());
                return true;
            }
            else if (KeyValidator.IsCtrlVKey(new KeyGesture(e.Key, e.KeyModifiers)))
            {
                e.Handled = true;
                CtrlvKey?.Invoke(this, new EventArgs());
                return true;
            }
            else if (KeyValidator.IsCtrlCKey(new KeyGesture(e.Key, e.KeyModifiers)))
            {
                e.Handled = true;
                CtrlcKey?.Invoke(this, new EventArgs());
                return true;
            }
            else if (KeyValidator.IsCtrlAKey(new KeyGesture(e.Key, e.KeyModifiers)))
            {
                e.Handled = true;
                CtrlaKey?.Invoke(this, new EventArgs());
                return true;
            }

            return false;
            #endregion
        }
        #endregion
    }
}
