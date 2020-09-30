using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class PropertiesVM : ExplorerPanelVM, IActivatableViewModel
    {
        protected object selectedInstance;
        public object SelectedInstance
        {
            get => selectedInstance;
            set => this.RaiseAndSetIfChanged(ref selectedInstance, value);
        }

        public override string Title => "Properties";
        public override string UrlPathSegment { get; } = "properties";
        public ViewModelActivator Activator { get; }
        public PropertiesVM() : this(null) { }
        public PropertiesVM(IScreen screen) : base(screen)
        {


            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
            });
        }
    }
}
