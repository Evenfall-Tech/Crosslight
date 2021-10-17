using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.API.IO.FileSystem;
using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.IO.FileSystem.Implementations;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.Language.CIL.Lang;
using Crosslight.Language.Viewer.ViewModels.Graph;
using Crosslight.Language.Viewer.ViewModels.Viewports;
using Crosslight.Language.Viewer.ViewModels.Windows;
using Crosslight.Language.Viewer.Views.Viewports;
using ReactiveUI;
using System.Linq;
using System.Reactive.Disposables;

namespace Crosslight.Language.Viewer
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

            IDirectory source = FileSystem.FromFiles(outPathStrings);

            CrosslightContext context = new CrosslightContext()
            {
                InputLanguage = new CILInputLanguage(),
                OutputLanguage = null,
            };

            Node ast = GetNodeFromFSItem(context.InputLanguage.Translate(source));
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

        private Node GetNodeFromFSItem(IFileSystemItem item)
        {
            if (item is IPhysicalFile) return null;
            if (item is IStringFile) return null;
            if (item is IFile file) return file.Content as Node;
            if (item is IDirectory directory)
            {
                foreach (var dirFile in directory.Items)
                {
                    var res = GetNodeFromFSItem(dirFile);
                    if (res != null) return res;
                }
                return null;
            }
            return null;
        }
    }
}
