﻿//////////////////////////////////////////////
// Apache 2.0  - 2016-2021
// Author      : Derek Tremblay (derektremblay666@gmail.com)
// Contributor : Janus Tida
// Contributor : ehsan69h
//////////////////////////////////////////////

using Avalonia;
using Avalonia.Input;
using Avalonia.Media;
using Crosslight.Common.UI.Controls.HexEditorControl.Core;
using Crosslight.Common.UI.Controls.HexEditorControl.Core.Bytes;
using Crosslight.Common.UI.Controls.HexEditorControl.Core.CharacterTable;
using Crosslight.Common.UI.Controls.HexEditorControl.Core.EventArguments;
using System;

namespace Crosslight.Common.UI.Controls.HexEditorControl
{
    internal class StringByte : BaseByte
    {
        #region Global class variables

        private bool _tblShowMte = true;
        private readonly bool _barchart = false;
        private readonly double _width = 12d;

        #endregion Global variable

        #region Contructor

        static StringByte()
        {
            AffectsRender<StringByte>(TextProperty);
        }

        public StringByte(HexEditor parent, bool barChart, double desiredWidth) : base(parent)
        {
            _barchart = barChart;
            _width = desiredWidth;
        }

        #endregion Contructor

        #region Properties

        /// <summary>
        /// Next Byte of this instance (used for TBL/MTE decoding)
        /// </summary>
        public byte? ByteNext { get; set; }

        #endregion Properties

        #region Characters tables

        /// <summary>
        /// Show or not Multi Title Enconding (MTE) are loaded in TBL file
        /// </summary>
        public bool TblShowMte
        {
            get => _tblShowMte;
            set
            {
                _tblShowMte = value;
                UpdateTextRenderFromByte();
            }
        }

        /// <summary>
        /// Type of caracter table are used un hexacontrol.
        /// For now, somes character table can be readonly but will change in future
        /// </summary>
        public CharacterTableType TypeOfCharacterTable { get; set; }

        /// <summary>
        /// Custom character table
        /// </summary>
        public TblStream TblCharacterTable { get; set; }

        /// <summary>
        /// This byte is an MTE
        /// </summary>
        public bool IsMTE { get; internal set; } = false;

        #endregion Characters tables

        #region Methods

        /// <summary>
        /// Update the render of text derived bytecontrol from byte property
        /// </summary>
        public override void UpdateTextRenderFromByte()
        {
            if (Byte != null)
            {
                var dteType = DteType.Invalid;

                switch (TypeOfCharacterTable)
                {
                    case CharacterTableType.Ascii:
                        Text = ByteConverters.ByteToChar(Byte.Byte[0]).ToString();
                        break;
                    case CharacterTableType.TblFile:
                        if (TblCharacterTable != null)
                        {
                            ReadOnlyMode = !TblCharacterTable.AllowEdit;

                            var content = "#";


                            if (TblShowMte && ByteNext.HasValue)
                                (content, dteType) = TblCharacterTable.FindMatch(ByteConverters.ByteToHex(Byte.Byte[0]) +
                                                                      ByteConverters.ByteToHex(ByteNext.Value), true);

                            if (content == "#")
                                content = TblCharacterTable.FindMatch(ByteConverters.ByteToHex(Byte.Byte[0]), true).text;

                            Text = content;
                        }
                        else
                            goto case CharacterTableType.Ascii;
                        break;
                }

                IsMTE = dteType == DteType.MultipleTitleEncoding;
            }
            else
                Text = string.Empty;
        }

        /// <summary>
        /// Update Background,foreground and font property
        /// </summary>
        public override void UpdateVisual()
        {
            if (IsSelected)
            {
                FontWeight = _parent.FontWeight;
                Foreground = _parent.ForegroundContrast;

                Background = FirstSelected ? _parent.SelectionFirstColor : _parent.SelectionSecondColor;
            }
            else if (IsHighLight)
            {
                FontWeight = _parent.FontWeight;
                Foreground = _parent.Foreground;
                Background = _parent.HighLightColor;
            }
            else if (Action != ByteAction.Nothing)
            {
                switch (Action)
                {
                    case ByteAction.Modified:
                        FontWeight = FontWeight.Bold;
                        Background = _parent.ByteModifiedColor;
                        Foreground = _parent.Foreground;
                        break;

                    case ByteAction.Deleted:
                        FontWeight = FontWeight.Bold;
                        Background = _parent.ByteDeletedColor;
                        Foreground = _parent.Foreground;
                        break;
                }
            }
            else
            {
                #region TBL COLORING
                var cbb = _parent.GetCustomBackgroundBlock(BytePositionInStream);

                Description = cbb != null ? cbb.Description : "";

                Background = cbb != null ? cbb.Color : Brushes.Transparent;
                FontWeight = _parent.FontWeight;
                Foreground = _parent.Foreground;

                if (TypeOfCharacterTable == CharacterTableType.TblFile)
                {
                    switch (Dte.TypeDte(Text))
                    {
                        case DteType.DualTitleEncoding:
                            Foreground = _parent.TbldteColor;
                            break;
                        case DteType.MultipleTitleEncoding:
                            Foreground = _parent.TblmteColor;
                            break;
                        case DteType.EndLine:
                            Foreground = _parent.TblEndLineColor;
                            break;
                        case DteType.EndBlock:
                            Foreground = _parent.TblEndBlockColor;
                            break;
                        default:
                            Foreground = _parent.TblDefaultColor;
                            break;
                    }
                }

                #endregion
            }

            UpdateAutoHighLiteSelectionByteVisual();

            InvalidateVisual();
        }

        /// <summary>
        /// Render the control
        /// </summary>
        public override void Render(DrawingContext dc)
        {
            if (_barchart)
            {
                Width = _width;
                Margin = new Thickness(2);

                #region Draw control
                //Draw background
                if (Background != null)
                    dc.DrawRectangle(Background, null, new Rect(0, 0, Bounds.Width, Bounds.Height));

                //Prevent drawing wrong graph
                if (Byte is null) return;

                //Draw chart
                int fillHeight = PercentValue * (int)_parent.LineHeight / 100;

                dc.DrawRectangle(_parent.BarChartColor,
                    null,
                    new Rect(0, _parent.LineHeight - fillHeight, Width, fillHeight));
                #endregion
            }
            else
            {
                Margin = new Thickness(0);

                base.Render(dc);

                #region Update width of control 

                //It's 8-10 time more fastest to update width on render for TBL string

                switch (TypeOfCharacterTable)
                {
                    case CharacterTableType.Ascii:
                        Width = _width;
                        break;
                    case CharacterTableType.TblFile:
                        Width = Byte is null
                            ? 0
                            : TextFormatted?.Bounds.Width > _width
                                ? TextFormatted.Bounds.Width
                                : _width;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                #endregion
            }
        }

        /// <summary>
        /// Clear control
        /// </summary>
        public override void Clear()
        {
            base.Clear();
            ByteNext = null;
        }

        #endregion Methods

        #region Events delegate

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (_barchart) return;
            if (Byte == null) return;
            if (KeyValidation(e)) return;

            //Other key exception
            if (e.Key == Key.CapsLock) return;
            if (e.Key == Key.Tab) return;

            //MODIFY ASCII...
            if (!_parent.IsLockedFile && (!ReadOnlyMode || !_parent.ReadOnlyMode))
            {
                var isok = false;

                if (Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled)
                {
                    if (e.KeyModifiers != KeyModifiers.Shift && e.KeyModifiers != KeyModifiers.Control &&
                        e.Key != Key.RightShift && e.Key != Key.LeftShift)
                    {
                        Text = KeyValidator.GetCharFromKey(e.Key).ToString();
                        isok = true;
                    }
                    else if (e.KeyModifiers == KeyModifiers.Shift && e.KeyModifiers != KeyModifiers.Control &&
                             e.Key != Key.RightShift && e.Key != Key.LeftShift)
                    {
                        isok = true;
                        Text = KeyValidator.GetCharFromKey(e.Key).ToString().ToLower();
                    }
                }
                else
                {
                    if (e.KeyModifiers != KeyModifiers.Shift && e.KeyModifiers != KeyModifiers.Control &&
                        e.Key != Key.RightShift && e.Key != Key.LeftShift)
                    {
                        Text = KeyValidator.GetCharFromKey(e.Key).ToString().ToLower();
                        isok = true;
                    }
                    else if (e.KeyModifiers == KeyModifiers.Shift && e.KeyModifiers != KeyModifiers.Control &&
                             e.Key != Key.RightShift && e.Key != Key.LeftShift)
                    {
                        isok = true;
                        Text = KeyValidator.GetCharFromKey(e.Key).ToString();
                    }
                }

                //Move focus event
                if (isok)
                {
                    Action = ByteAction.Modified;

                    Byte.ChangeByteValue(ByteConverters.CharToByte(Text[0]), BytePositionInStream);

                    //Insert byte at end of file
                    if (_parent.Length == BytePositionInStream + 1)
                    {
                        byte[] byteToAppend = { 0 };
                        _parent.AppendByte(byteToAppend);
                    }

                    OnMoveNext(new ByteEventArgs(BytePositionInStream));
                }
            }

            base.OnKeyDown(e);
        }

        #endregion Events delegate

        #region Caret events

        protected override void OnGotFocus(GotFocusEventArgs e)
        {
            //TODO: complete caret implemention ....

            _parent.SetCaretMode(CaretMode.Overwrite);

            if (ReadOnlyMode || Byte == null)
                _parent.HideCaret();
            else
            {
                var matrix = this.TransformToVisual(_parent);
                if (matrix.HasValue)
                    _parent.MoveCaret(new Point(0, 0).Transform(matrix.Value));
            }

            base.OnGotFocus(e);
        }
        #endregion

        #region BarChart Support

        public static readonly StyledProperty<int> TextProperty =
            AvaloniaProperty.Register<StringByte, int>(nameof(PercentValue), defaultValue: 0);

        /// <summary>
        /// Defines the Value
        /// </summary>
        public int PercentValue
        {
            get => (int)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion
    }
}
