using Crosslight.API.IO;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class ExecuteVM : ExplorerPanelVM, IActivatableViewModel
    {
        public new const string ConstTitle = "Execute";

        public ReactiveCommand<Unit, object> Decode { get; }

        public override string Title => ConstTitle;
        public override string UrlPathSegment { get; } = "execute";
        public ViewModelActivator Activator { get; }
        public ExecuteVM() : this(null) { }
        public ExecuteVM(IScreen screen) : base(screen)
        {
            Decode = ReactiveCommand.Create<object>(() =>
            {
                InputLanguage language = 
                    Locator.Current
                    .GetService<ExplorerLocator>()
                    .Open<LanguagesVM>()?
                    .SelectedInputLanguage?
                    .InputLanguage;
                
                Source src = Source.FromSources(
                    Locator.Current
                    .GetService<ExplorerLocator>()
                    .Open<SourceInputVM>()?
                    .SelectedSources?
                    .Select(s => s.Source));

                Node nodeRoot = language.Decode(src);
                return (object)nodeRoot;
            });

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
            });
        }
    }
}
