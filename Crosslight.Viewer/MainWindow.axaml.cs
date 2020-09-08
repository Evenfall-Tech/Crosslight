using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Crosslight.API.IO;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.CIL.Lang;
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
        private readonly GraphViewer viewer;
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

        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Choose input files",
                AllowMultiple = true
            };
            var outPathStrings = await openFileDialog.ShowAsync(this);

            Source source = Source.FromFiles(outPathStrings);

            CrosslightContext context = new CrosslightContext()
            {
                InputLanguage = new CILInputLanguage(),
                OutputLanguage = null,
            };

            Node ast = context.InputLanguage.Decode(source);
            if (ast == null)
            {
                return;
            }

            viewer.DataContext = new GraphViewerViewModel(ast);
        }
    }
}
