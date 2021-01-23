using Crosslight.API.IO.FileSystem;
using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using ReactiveUI;
using Splat;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class ExecuteVM : ExplorerPanelVM, IActivatableViewModel
    {
        public new const string ConstTitle = "Execute";

        public ReactiveCommand<Unit, IFileSystemItem> Decode { get; }
        public ReactiveCommand<Unit, IFileSystemItem> Encode { get; }

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
                ILanguage language =
                    locator
                    .Open<LanguagesVM>()?
                    .SelectedInputLanguage?
                    .InputLanguage;
                // TODO: refactor these for virtual file system
                IFileSystemItem src = FileSystem.FromItems(
                    locator
                    .Open<SourceInputVM>()?
                    .SelectedSources?
                    .Select(s => s.Source));

                return language.Translate(src);
            });
            Encode = ReactiveCommand.Create(() =>
            {
                var locator =
                    Locator.Current
                    .GetService<ExplorerLocator>();
                ILanguage language =
                    locator
                    .Open<LanguagesVM>()?
                    .SelectedOutputLanguage?
                    .OutputLanguage;
                // TODO: refactor these for virtual file system
                IFileSystemItem src =
                    locator.Open<ResultListVM>()
                    .SelectedIntermediateResults
                    .FirstOrDefault()
                    .Result;

                return language.Translate(src);
            });

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
            });
        }
    }
}
