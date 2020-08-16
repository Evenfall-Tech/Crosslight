using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Crosslight.Viewer.Mock;
using Crosslight.Viewer.Models.Graph;
using Crosslight.Viewer.Nodes;

namespace Crosslight.Viewer
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
