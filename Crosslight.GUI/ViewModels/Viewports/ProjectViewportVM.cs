using Crosslight.GUI.ViewModels.Explorers;
using DynamicData;
using ReactiveUI;
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
        public ReadOnlyObservableCollection<ExplorerContainerVM> Containers => containers;

        public ViewModelActivator Activator { get; }
        public ProjectViewportVM()
        {
            containerSource = new SourceList<ExplorerContainerVM>();

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
