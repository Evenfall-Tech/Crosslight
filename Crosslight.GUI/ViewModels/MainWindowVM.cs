using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Viewports;
using Dock.Model.Controls;
using DynamicData;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Disposables;

namespace Crosslight.GUI.ViewModels
{
    public class MainWindowVM : BaseViewModel, IActivatableViewModel
    {
        private MainViewportVM mainViewport;
        public MainViewportVM MainViewport
        {
            get => mainViewport;
            set => this.RaiseAndSetIfChanged(ref mainViewport, value);
        }

        public ViewModelActivator Activator { get; }
        public MainWindowVM()
        {
            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                var factory = new ProjectViewportFactory(new object());
                MainViewport = new MainViewportVM()
                {
                    Factory = factory,
                    Layout = factory.CreateLayout() as IRootDock,
                };
                if (MainViewport.Layout != null)
                {
                    MainViewport.Factory?.InitLayout(mainViewport.Layout);
                    if (MainViewport.Layout is { } root)
                    {
                        root.Navigate("Home");
                    }
                }
            });
        }
    }
}
