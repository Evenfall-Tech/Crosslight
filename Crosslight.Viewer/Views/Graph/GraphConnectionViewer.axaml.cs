using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Crosslight.Viewer.Views.Graph
{
    public class GraphConnectionViewer : UserControl
    {
        public GraphConnectionViewer()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
