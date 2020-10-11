using Crosslight.GUI.ViewModels.Explorers;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Text;

namespace Crosslight.GUI.ViewModels.Viewports
{
    public class ProjectViewportVM : BaseViewModel, IActivatableViewModel
    {
        protected SourceList<ExplorerContainerVM> containerSource;
        protected ReadOnlyObservableCollection<ExplorerContainerVM> containers;
        protected ObservableCollectionExtended<MenuItemVM> menuItems;
        public ReadOnlyObservableCollection<ExplorerContainerVM> Containers => containers;
        public ObservableCollectionExtended<MenuItemVM> MenuItems
        {
            get => menuItems;
            set => this.RaiseAndSetIfChanged(ref menuItems, value);
        }

        public ViewModelActivator Activator { get; }
        public ProjectViewportVM()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new ExplorerLocator(this));

            containerSource = new SourceList<ExplorerContainerVM>();
            menuItems = new ObservableCollectionExtended<MenuItemVM>();

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                containerSource.Connect()
                    .Bind(out containers)
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }

        public void AddExplorer(ExplorerContainerVM vm) => containerSource.Add(vm);
        public void RemoveExplorer(ExplorerContainerVM vm) => containerSource.Remove(vm);
    }
}
