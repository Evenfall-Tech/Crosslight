using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class SourceInputVM : ExplorerPanelVM, IActivatableViewModel
    {
        public override string Title => "Source Input";
        public override string UrlPathSegment { get; } = "source_input";
        public ViewModelActivator Activator { get; }
        public SourceInputVM() : this(null) { }
        public SourceInputVM(IScreen screen) : base(screen)
        {
            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
            });
        }
    }
}
