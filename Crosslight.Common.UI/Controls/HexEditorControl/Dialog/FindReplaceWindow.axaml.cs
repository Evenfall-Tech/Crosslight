//////////////////////////////////////////////
// Apache 2.0 - 2018-2019
// Author : Derek Tremblay (derektremblay666@gmail.com)
//////////////////////////////////////////////

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.IO;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Dialog
{
    /// <summary>
    /// Logique d'interaction pour FindReplaceWindow.xaml
    /// </summary>
    public class FindReplaceWindow : Window
    {
        private readonly HexEditor _parent;

        public Popup SettingPopup => this.FindControl<Popup>("SettingPopup");
        public MenuItem HighlightMenuItem => this.FindControl<MenuItem>("HighlightMenuItem");
        public MenuItem TrimMenuItem => this.FindControl<MenuItem>("TrimMenuItem");
        public Button FindNextButton => this.FindControl<Button>("FindNextButton");
        public Button CloseButton => this.FindControl<Button>("CloseButton");
        public Button ReplaceButton => this.FindControl<Button>("ReplaceButton");
        public Button ReplaceAllButton => this.FindControl<Button>("ReplaceAllButton");
        public Button FindAllButton => this.FindControl<Button>("FindAllButton");
        public Button FindFirstButton => this.FindControl<Button>("FindFirstButton");
        public Button FindLastButton => this.FindControl<Button>("FindLastButton");
        public Button ClearButton => this.FindControl<Button>("ClearButton");
        public Button ClearReplaceButton => this.FindControl<Button>("ClearReplaceButton");
        public Button ReplaceNextButton => this.FindControl<Button>("ReplaceNextButton");
        public Button SettingButton => this.FindControl<Button>("SettingButton");
        public HexEditor FindHexEdit => this.FindControl<HexEditor>("FindHexEdit");
        public HexEditor ReplaceHexEdit => this.FindControl<HexEditor>("ReplaceHexEdit");
        public FindReplaceWindow(HexEditor parent, byte[] findData = null)
        {
            InitializeComponent();

            HighlightMenuItem.Click += SettingMenuItem_Click;
            TrimMenuItem.Click += SettingMenuItem_Click;
            FindNextButton.Click += FindNextButton_Click;
            CloseButton.Click += CloseButton_Click;
            ReplaceButton.Click += ReplaceButton_Click;
            ReplaceAllButton.Click += ReplaceAllButton_Click;
            FindAllButton.Click += FindAllButton_Click;
            FindFirstButton.Click += FindFirstButton_Click;
            FindLastButton.Click += FindLastButton_Click;
            ClearButton.Click += ClearButton_Click;
            ClearReplaceButton.Click += ClearReplaceButton_Click;
            ReplaceNextButton.Click += ReplaceNextButton_Click;
            SettingButton.Click += SettingButton_Click;
            FindHexEdit.BytesDeleted += FindHexEdit_BytesDeleted;
            ReplaceHexEdit.BytesDeleted += ReplaceHexEdit_BytesDeleted;

            //Parent hexeditor for "binding" search
            _parent = parent;

            InitializeMStream(FindHexEdit, findData);
            InitializeMStream(ReplaceHexEdit);
        }

        #region Events
        private void ClearButton_Click(object sender, RoutedEventArgs e) => InitializeMStream(FindHexEdit);
        private void ClearReplaceButton_Click(object sender, RoutedEventArgs e) => InitializeMStream(ReplaceHexEdit);
        private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();

        private void FindAllButton_Click(object sender, RoutedEventArgs e) =>
            _parent?.FindAll(FindHexEdit.GetAllBytes(), HighlightMenuItem.IsSelected);

        private void FindFirstButton_Click(object sender, RoutedEventArgs e) =>
            _parent?.FindFirst(FindHexEdit.GetAllBytes(), 0, HighlightMenuItem.IsSelected);

        private void FindNextButton_Click(object sender, RoutedEventArgs e) =>
            _parent?.FindNext(FindHexEdit.GetAllBytes(), HighlightMenuItem.IsSelected);

        private void FindLastButton_Click(object sender, RoutedEventArgs e) =>
            _parent?.FindLast(FindHexEdit.GetAllBytes(), HighlightMenuItem.IsSelected);

        private void ReplaceButton_Click(object sender, RoutedEventArgs e) =>
            _parent?.ReplaceFirst(FindHexEdit.GetAllBytes(), ReplaceHexEdit.GetAllBytes(),
                TrimMenuItem.IsSelected, HighlightMenuItem.IsSelected);

        private void ReplaceNextButton_Click(object sender, RoutedEventArgs e) =>
            _parent?.ReplaceNext(FindHexEdit.GetAllBytes(), ReplaceHexEdit.GetAllBytes(),
               TrimMenuItem.IsSelected, HighlightMenuItem.IsSelected);

        private void ReplaceAllButton_Click(object sender, RoutedEventArgs e) =>
            _parent?.ReplaceAll(FindHexEdit.GetAllBytes(), ReplaceHexEdit.GetAllBytes(),
                TrimMenuItem.IsSelected, HighlightMenuItem.IsSelected);

        private void ReplaceHexEdit_BytesDeleted(object sender, System.EventArgs e) =>
            InitializeMStream(ReplaceHexEdit, ReplaceHexEdit.GetAllBytes());

        private void FindHexEdit_BytesDeleted(object sender, System.EventArgs e) =>
            InitializeMStream(FindHexEdit, FindHexEdit.GetAllBytes());

        private void SettingButton_Click(object sender, RoutedEventArgs e) => SettingPopup.IsOpen = true;

        private void SettingMenuItem_Click(object sender, RoutedEventArgs e) => SettingPopup.IsOpen = false;
        #endregion

        #region Methods
        /// <summary>
        /// Initialize stream and hexeditor
        /// </summary>
        private void InitializeMStream(HexEditor hexeditor, byte[] findData = null)
        {
            hexeditor.CloseProvider();

            var ms = new MemoryStream(1);

            if (findData != null && findData.Length > 0)
                foreach (byte b in findData)
                    ms.WriteByte(b);
            else
                ms.WriteByte(0);

            hexeditor.Stream = ms;
        }
        #endregion

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
