using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.Nodes;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Explorers.Items;
using Crosslight.Language.Viewer.ViewModels.Graph;
using Crosslight.Language.Viewer.Views.Graph;
using ReactiveUI;
using Splat;
using System;
using System.IO;
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
                    .Select(x => ControlFromFile(x))
                    .BindTo(this, x => x.Content)
                    .DisposeWith(disp);
            });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private object ControlFromFile(IFileSystemItem fileSystemItem)
        {
            if (fileSystemItem == null)
            {
                return null;
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

        private object FillFile(IFile file)
        {
            if (file == null || file.Content == null)
            {
                return null;
            }
            else if (file.Content is Node node)
            {
                return new GraphViewer()
                {
                    ViewModel = new GraphViewerViewModel()
                    {
                        RootNode = node,
                    }
                };
            }
            else if (file.Content is ILogical logical)
            {
                return logical;
            }
            else return file.Content.ToString();
            return Unit.Default;
        }

        private object FillDirectory(IDirectory directory)
        {
            var view = Locator.Current.GetService<IViewFor<ResultItemVM>>();
            string fullPath = Path.GetFullPath(directory.Name).TrimEnd(Path.DirectorySeparatorChar);
            string name = Path.GetFileName(fullPath);
            view.ViewModel = new ResultItemVM()
            {
                Name = name,
                Result = directory,
            };
            return view;
        }
    }
}
