using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Dialog
{
    public class GiveByteWindow : Window
    {
        public Button OkButton => this.FindControl<Button>("OkButton");
        public HexBox HexTextBox => this.FindControl<HexBox>("HexTextBox");
        public GiveByteWindow()
        {
            InitializeComponent();

            OkButton.Click += OKButton_Click;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e) => Close(true);

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
