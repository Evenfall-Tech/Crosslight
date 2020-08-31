using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Crosslight.Viewer.Mock;
using Crosslight.Viewer.Models.Graph;
using Crosslight.Viewer.Nodes;
using Crosslight.Viewer.ViewModels.Graph;
using Crosslight.Viewer.Views.Graph;
using System.Threading.Tasks;

namespace Crosslight.Viewer
{
    public class MainWindow : Window
    {
        private GraphViewer viewer;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            viewer = this.FindControl<GraphViewer>("myGraphViewer");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var task = Task.Run(async () =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Choose input files",
                    AllowMultiple = true
                };
                var outPathStrings = await openFileDialog.ShowAsync(this).ConfigureAwait(false);
                viewer.DataContext = new GraphViewerViewModel(outPathStrings);
                return outPathStrings;
            });
        }
    }
}
