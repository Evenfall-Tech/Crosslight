using Crosslight.GUI.ViewModels.Explorers;
using Dock.Model;
using Dock.Model.Controls;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;

namespace Crosslight.GUI.ViewModels.Viewports
{
    public class ProjectViewportVM : RootDock, IActivatableViewModel
    {
        protected SourceList<ExplorerContainerVM> containerSource;
        protected ReadOnlyObservableCollection<ExplorerContainerVM> containers;

        public ReadOnlyObservableCollection<ExplorerContainerVM> Containers => containers;

        public ViewModelActivator Activator { get; }
        public ProjectViewportVM()
        {
            Id = nameof(ProjectViewportVM);
            Title = nameof(ProjectViewportVM);

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
        public override IDockable Clone()
        {
            var projectVM = new ProjectViewportVM();

            CloneHelper.CloneDockProperties(this, projectVM);
            CloneHelper.CloneRootDockProperties(this, projectVM);

            return projectVM;
        }

        public void AddExplorer(ExplorerContainerVM vm) => containerSource.Add(vm);
        public void RemoveExplorer(ExplorerContainerVM vm) => containerSource.Remove(vm);
    }
}
