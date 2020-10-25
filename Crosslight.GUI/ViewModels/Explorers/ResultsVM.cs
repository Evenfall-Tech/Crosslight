using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class ResultsVM : ExplorerPanelVM, IActivatableViewModel
    {
        public new const string ConstTitle = "Results";
        protected object result;
        protected string id;

        public object Result
        {
            get => result;
            set => this.RaiseAndSetIfChanged(ref result, value);
        }

        public override string Title => $"{ConstTitle} {id}";
        public override string UrlPathSegment { get; }
        public ViewModelActivator Activator { get; }
        public ResultsVM() : this(null) { }
        public ResultsVM(IScreen screen) : base(screen)
        {
            id = Guid.NewGuid().ToString("N").ToUpper().Substring(0, 5);
            UrlPathSegment = $"execute_{id}";

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
            });
        }
    }
}
