using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.Nodes;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.Viewer.ViewModels.Graph;
using Crosslight.Viewer.Views.Graph;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.Views.Explorers
{
    public class Results : ReactiveUserControl<ResultsVM>
    {
        public Results()
        {
            this.WhenActivated(disp =>
            {
                this.WhenAnyValue(x => x.ViewModel.Result)
                    .DistinctUntilChanged()
                    .Select(x => FillContent(x))
                    .Subscribe()
                    .DisposeWith(disp);
            });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private Unit FillContent(IFileSystemItem fileSystemItem)
        {
            if (fileSystemItem == null)
            {
                //Content = null;
                return Unit.Default;
            }
            else if (fileSystemItem is IFile file)
            {
                return FillFile(file);
            }
            else if (fileSystemItem is IDirectory directory)
            {
                return FillDirectory(directory);
            }
            else throw new NotImplementedException($"{fileSystemItem.GetType().Name} is not yet supported.");
        }

        private Unit FillFile(IFile file)
        {
            if (file == null || file.Content == null)
            {
                //Content = null;
                return Unit.Default;
            }
            else if (file.Content is Node node)
            {
                Content = new GraphViewer()
                {
                    ViewModel = new GraphViewerViewModel()
                    {
                        RootNode = node,
                    }
                };
            }
            else if (file.Content is ILogical logical)
            {
                Content = logical;
            }
            else Content = file.Content.ToString();
            return Unit.Default;
        }

        private Unit FillDirectory(IDirectory directory)
        {
            Content = directory.Name;
            return Unit.Default;
        }
    }
}
