using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.IO;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Dialog
{
    public class FindWindow : Window
    {
        private MemoryStream _findMs = new MemoryStream(1);
        private readonly HexEditor _parent;

        public Button FindNextButton => this.FindControl<Button>("FindNextButton");
        public Button CloseButton => this.FindControl<Button>("CloseButton");
        public Button FindAllButton => this.FindControl<Button>("FindAllButton");
        public Button FindFirstButton => this.FindControl<Button>("FindFirstButton");
        public Button FindLastButton => this.FindControl<Button>("FindLastButton");
        public Button ClearButton => this.FindControl<Button>("ClearButton");
        public HexEditor FindHexEdit => this.FindControl<HexEditor>("FindHexEdit");
        public FindWindow(HexEditor parent, byte[] findData = null)
        {
            InitializeComponent();
            
            FindNextButton.Click += FindNextButton_Click;
            CloseButton.Click += CloseButton_Click;
            FindAllButton.Click += FindAllButton_Click;
            FindFirstButton.Click += FindFirstButton_Click;
            FindLastButton.Click += FindLastButton_Click;
            ClearButton.Click += ClearButton_Click;
            FindHexEdit.BytesDeleted += FindHexEdit_BytesDeleted;

            //Parent hexeditor for "binding" search
            _parent = parent;

            InitializeMStream(findData);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();
        private void ClearButton_Click(object sender, RoutedEventArgs e) => InitializeMStream();

        private void FindHexEdit_BytesDeleted(object sender, System.EventArgs e) =>
            InitializeMStream(FindHexEdit.GetAllBytes());

        private void FindAllButton_Click(object sender, RoutedEventArgs e) =>
            _parent?.FindAll(FindHexEdit.GetAllBytes(), true);

        private void FindFirstButton_Click(object sender, RoutedEventArgs e) =>
            _parent?.FindFirst(FindHexEdit.GetAllBytes());

        private void FindNextButton_Click(object sender, RoutedEventArgs e) =>
            _parent?.FindNext(FindHexEdit.GetAllBytes());

        private void FindLastButton_Click(object sender, RoutedEventArgs e) =>
            _parent?.FindLast(FindHexEdit.GetAllBytes());

        /// <summary>
        /// Initialize stream and hexeditor
        /// </summary>
        private void InitializeMStream(byte[] findData = null)
        {
            FindHexEdit.CloseProvider();

            _findMs = new MemoryStream(1);

            if (findData != null && findData.Length > 0)
                foreach (byte b in findData)
                    _findMs.WriteByte(b);
            else
                _findMs.WriteByte(0);

            FindHexEdit.Stream = _findMs;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
