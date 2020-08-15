using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Crosslight.Viewer.ViewModels.Graph;

namespace Crosslight.Viewer.Views.Graph
{
    public class GraphViewer : UserControl
    {
        private readonly Canvas canvas;
        public GraphViewer()
        {
            this.InitializeComponent();
            canvas = this.FindControl<Canvas>("graphCanvas");
            DataContextChanged += GraphViewer_DataContextChanged;
        }

        private void GraphViewer_DataContextChanged(object sender, System.EventArgs e)
        {
            if (!(DataContext is GraphViewerViewModel)) return;
            canvas.Children.Clear();
            foreach (var node in (DataContext as GraphViewerViewModel).GraphViewModel.Nodes)
            {
                if (!(node.Data is Control control))
                {
                    control = new TextBlock()
                    {
                        Text = node.Data.ToString(),
                    };
                }
                Canvas.SetLeft(control, node.Left);
                Canvas.SetTop(control, node.Top);
                canvas.Children.Add(control);
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
