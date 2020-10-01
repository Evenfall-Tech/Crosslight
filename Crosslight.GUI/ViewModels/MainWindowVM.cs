using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Viewports;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Disposables;

namespace Crosslight.GUI.ViewModels
{
    public class MainWindowVM : BaseViewModel, IActivatableViewModel
    {
        private ProjectViewportVM project;
        public ProjectViewportVM Project
        {
            get => project;
            set => this.RaiseAndSetIfChanged(ref project, value);
        }

        public ViewModelActivator Activator { get; }
        public MainWindowVM()
        {
            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                project = new ProjectViewportVM();
            });
        }
    }
}
