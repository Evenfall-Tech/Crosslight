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

        public ReactiveCommand<Unit, Node> Decode { get; }
        public ReactiveCommand<Unit, object> Encode { get; }

        public override string Title => ConstTitle;
        public override string UrlPathSegment { get; } = "execute";
        public ViewModelActivator Activator { get; }
        public ExecuteVM() : this(null) { }
        public ExecuteVM(IScreen screen) : base(screen)
        {
            Decode = ReactiveCommand.Create(() =>
            {
                var locator =
                    Locator.Current
                    .GetService<ExplorerLocator>();
                InputLanguage language =
                    locator
                    .Open<LanguagesVM>()?
                    .SelectedInputLanguage?
                    .InputLanguage;

                Source src = Source.FromSources(
                    locator
                    .Open<SourceInputVM>()?
                    .SelectedSources?
                    .Select(s => s.Source));

                Node nodeRoot = language.Decode(src);
                return nodeRoot;
            });
            Encode = ReactiveCommand.Create<object>(() =>
            {
                var locator =
                    Locator.Current
                    .GetService<ExplorerLocator>();
                OutputLanguage language =
                    locator
                    .Open<LanguagesVM>()?
                    .SelectedOutputLanguage?
                    .OutputLanguage;

                Node src = 
                    locator.Open<ResultListVM>()
                    .SelectedIntermediateResults
                    .FirstOrDefault(x => x.Result is Node)
                    .Result as Node;

                return language.Encode(src);
            });

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
            });
        }
    }
}
