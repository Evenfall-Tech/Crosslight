using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.API.IO;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.CIL.Lang;
using Crosslight.Viewer.ViewModels.Graph;
using Crosslight.Viewer.ViewModels.Viewports;
using Crosslight.Viewer.ViewModels.Windows;
using Crosslight.Viewer.Views.Graph;
using Crosslight.Viewer.Views.Viewports;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Crosslight.Viewer
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        private ViewerViewport ViewerViewport => this.FindControl<ViewerViewport>("viewerViewport");
        public MainWindow()
        {
            this.WhenActivated(disposables =>
            {
                this.Bind(this.ViewModel, x => x.ViewportViewModel, x => x.ViewerViewport.ViewModel)
                    .DisposeWith(disposables);
            });
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
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
            if (outPathStrings.Length == 0) return;

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

            ViewModel.ViewportViewModel = new ViewerViewportViewModel()
            {
                GraphViewModel = new GraphViewerViewModel()
                {
                    RootNode = ast,
                }
            };
        }
    }
}
