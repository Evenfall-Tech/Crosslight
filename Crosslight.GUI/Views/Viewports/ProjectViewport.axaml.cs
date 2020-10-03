using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Viewports;
using Crosslight.GUI.Views.Explorers;
using ReactiveUI;
using Splat;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.Views.Viewports
{
    public class ProjectViewport : ReactiveUserControl<ProjectViewportVM>
    {
        public ItemsControl ExplorerContainer => this.FindControl<ItemsControl>("explorerContainer");
        public ProjectViewport()
        {
            Locator.CurrentMutable.Register(() => new ExplorerContainer(), typeof(IViewFor<ExplorerContainerVM>));
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, x => x.Containers, x => x.ExplorerContainer.Items)
                    .DisposeWith(disposables);

                this.WhenAnyValue(x => x.ViewModel)
                    .Where(x => x != null)
                    .DistinctUntilChanged()
                    .Subscribe(y =>
                    {
                        OpenExplorer(x =>
                        {
                            var vm = Locator.Current.GetService<LanguagesVM>();
                            vm.SetHostScreen(x);
                            return vm;
                        });
                        OpenExplorer(x =>
                        {
                            var vm = Locator.Current.GetService<PropertiesVM>();
                            vm.SetHostScreen(x);
                            return vm;
                        });
                        OpenExplorer(x =>
                        {
                            var vm = Locator.Current.GetService<SourceInputVM>();
                            vm.SetHostScreen(x);
                            return vm;
                        });
                    })
                    .DisposeWith(disposables);
            });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OpenExplorer(Func<IScreen, ExplorerPanelVM> explorer)
        {
            var container = new ExplorerContainerVM();
            ViewModel.AddExplorer(container);
            container.GoNext.Execute(explorer);
        }
    }
}
