using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
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

        private Unit FillContent(object content)
        {
            if (content == null)
            {
                //Content = null;
                return Unit.Default;
            }
            else if (content is Node node)
            {
                Content = new GraphViewer()
                {
                    ViewModel = new GraphViewerViewModel()
                    {
                        RootNode = node,
                    }
                };
            }
            else Content = content.ToString();
            return Unit.Default;
        }
    }
}
