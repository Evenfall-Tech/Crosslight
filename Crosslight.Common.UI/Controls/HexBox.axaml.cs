//////////////////////////////////////////////
// Apache 2.0  - 2017-2019
// Author : Derek Tremblay (derektremblay666@gmail.com)
//////////////////////////////////////////////

using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Crosslight.Common.UI.Controls.HexEditorControl.Core;
using Crosslight.Common.UI.Controls.HexEditorControl.Core.Bytes;
using System;

namespace Crosslight.Common.UI.Controls
{
    /// <summary>
    /// Control for enter hex value and deal with.
    /// </summary>
    public class HexBox : UserControl
    {
        #region Events
        /// <summary>
        /// Occurs when value are changed.
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Get hexadecimal value of LongValue
        /// </summary>
        public string HexValue => ByteConverters.LongToHex(LongValue);

        /// <summary>
        /// Get or set maximum value
        /// </summary>
        public long MaximumValue
        {
            get => (long)GetValue(MaximumValueProperty);
            set => SetValue(MaximumValueProperty, value);
        }

        public static readonly StyledProperty<long> MaximumValueProperty =
            AvaloniaProperty.Register<HexBox, long>(nameof(MaximumValue), defaultValue: long.MaxValue);

        private static void MaximumValue_Changed(IAvaloniaObject d, AvaloniaPropertyChangedEventArgs e)
        {
            if (!(d is HexBox ctrl)) return;
            if (e.NewValue == e.OldValue) return;
            if (ctrl.LongValue <= (long)e.NewValue) return;

            ctrl.UpdateValueFrom((long)e.NewValue);
        }

        /// <summary>
        /// Get or set the hex value show in control
        /// </summary>
        public long LongValue
        {
            get => (long)GetValue(LongValueProperty);
            set => SetValue(LongValueProperty, value);
        }

        public static readonly StyledProperty<long> LongValueProperty =
            AvaloniaProperty.Register<HexBox, long>(nameof(LongValue), defaultValue: 0L, coerce: LongValue_CoerceValue);

        private static long LongValue_CoerceValue(IAvaloniaObject d, long baseValue)
        {
            var ctrl = d as HexBox;

            var newValue = (long)baseValue;

            if (newValue > ctrl.MaximumValue) newValue = ctrl.MaximumValue;
            if (newValue < 0) newValue = 0;

            return newValue;
        }

        private static void LongValue_Changed(IAvaloniaObject d, AvaloniaPropertyChangedEventArgs e)
        {
            if (!(d is HexBox ctrl)) return;
            if (e.NewValue == e.OldValue) return;

            var val = ByteConverters.LongToHex((long)e.NewValue);

            if (val == "00000000")
                val = "0";
            else if (val.Length >= 3)
                val = val.TrimStart('0');

            ctrl.HexTextBox.Text = val.ToUpperInvariant();
            ctrl.HexTextBox.CaretIndex = ctrl.HexTextBox.Text.Length;
            ToolTip.SetTip(ctrl, e.NewValue);

            ctrl.ValueChanged?.Invoke(ctrl, new EventArgs());
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Substract one to the LongValue
        /// </summary>
        private void SubstractOne() => LongValue--;

        /// <summary>
        /// Add one to the LongValue
        /// </summary>
        private void AddOne() => LongValue++;

        /// <summary>
        /// Update value from decimal long
        /// </summary>
        private void UpdateValueFrom(long value) => LongValue = value;

        /// <summary>
        /// Update value from hex string
        /// </summary>
        private void UpdateValueFrom(string value)
        {
            var (success, val) = ByteConverters.HexLiteralToLong(value);

            LongValue = success ? val : 0;
        }

        #endregion Methods

        #region Controls events

        private void HexTextBox_PreviewKeyDown(object sender, KeyEventArgs e) =>
            e.Handled = !KeyValidator.IsHexKey(e.Key) &&
                !KeyValidator.IsBackspaceKey(e.Key) &&
                !KeyValidator.IsDeleteKey(e.Key) &&
                !KeyValidator.IsArrowKey(e.Key) &&
                !KeyValidator.IsTabKey(e.Key) &&
                !KeyValidator.IsEnterKey(e.Key);

        private void HexTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                AddOne();

            if (e.Key == Key.Down)
                SubstractOne();

            HexTextBox.Focus();
        }

        private void UpButton_Click(object sender, RoutedEventArgs e) => AddOne();

        private void DownButton_Click(object sender, RoutedEventArgs e) => SubstractOne();

        private void HexTextBox_TextChanged(object sender, AvaloniaPropertyChangedEventArgs<string> e) =>
            UpdateValueFrom(HexTextBox.Text);

        private void CopyHexaMenuItem_Click(object sender, RoutedEventArgs e) =>
            Application.Current.Clipboard.SetTextAsync($"0x{HexTextBox.Text}");

        private void CopyLongMenuItem_Click(object sender, RoutedEventArgs e) =>
            Application.Current.Clipboard.SetTextAsync(LongValue.ToString());

        #endregion Controls events

        public Button UpButton => this.FindControl<Button>("UpButton");
        public Button DownButton => this.FindControl<Button>("DownButton");
        public TextBox HexTextBox => this.FindControl<TextBox>("HexTextBox");
        public MenuItem CopyHexaMenuItem => this.FindControl<MenuItem>("CopyHexaMenuItem");
        public MenuItem CopyLongMenuItem => this.FindControl<MenuItem>("CopyLongMenuItem");

        static HexBox()
        {
            MaximumValueProperty.Changed.AddClassHandler<HexBox>((x, e) => MaximumValue_Changed(x, e));
            LongValueProperty.Changed.AddClassHandler<HexBox>((x, e) => LongValue_Changed(x, e));
        }

        public HexBox()
        {
            InitializeComponent();

            UpButton.Click += UpButton_Click;
            DownButton.Click += DownButton_Click;
            CopyHexaMenuItem.Click += CopyHexaMenuItem_Click;
            CopyLongMenuItem.Click += CopyLongMenuItem_Click;
            AddHandler(TextBox.KeyDownEvent, HexTextBox_KeyDown);
            // TODO: check if direct applies here
            AddHandler(TextBox.KeyDownEvent, HexTextBox_PreviewKeyDown, RoutingStrategies.Tunnel | RoutingStrategies.Direct);
            HexTextBox.GetPropertyChangedObservable(TextBox.TextProperty)
                .Subscribe(args => HexTextBox_TextChanged(HexTextBox, args as AvaloniaPropertyChangedEventArgs<string>));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
