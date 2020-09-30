using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.Views.Explorers;
using ReactiveUI;
using Splat;
using System;
using System.Reactive.Disposables;

namespace Crosslight.GUI.Views
{
    public class MainWindow : ReactiveWindow<MainWindowVM>
    {
        public ItemsControl ExplorerContainer => this.FindControl<ItemsControl>("explorerContainer");
        public MainWindow()
        {
            Locator.CurrentMutable.Register(() => new ExplorerContainer(), typeof(IViewFor<ExplorerContainerVM>));
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, x => x.Containers, x => x.ExplorerContainer.Items)
                    .DisposeWith(disposables);

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

        private void OpenExplorer(Func<IScreen, ExplorerPanelVM> explorer)
        {
            var container = new ExplorerContainerVM();
            ViewModel.AddExplorer(container);
            container.GoNext.Execute(explorer);
        }
    }
}
