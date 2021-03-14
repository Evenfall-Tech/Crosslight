using Crosslight.API.IO.FileSystem.Abstractions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class ResultsVM : ExplorerPanelVM, IActivatableViewModel
    {
        public new const string ConstTitle = "Result";
        protected IFileSystemItem result;
        protected readonly ObservableAsPropertyHelper<string> idObservable;

        public IFileSystemItem Result
        {
            get => result;
            set => this.RaiseAndSetIfChanged(ref result, value);
        }

        private string IdValue => idObservable.Value;
        public override string UrlPathSegment => $"result_{Id}";
        public ViewModelActivator Activator { get; }
        public ResultsVM() : this(null) { }
        public ResultsVM(IScreen screen) : base(screen)
        {
            idObservable = this.WhenAnyValue(x => x.Result)
                .DistinctUntilChanged()
                .Where(x => x != null)
                .Select(x => GenerateID(x))
                .ToProperty(this, x => x.IdValue);

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                this.WhenAnyValue(x => x.IdValue)
                    .BindTo(this, x => x.Id)
                    .DisposeWith(disposables);
                this.WhenAnyValue(x => x.Id)
                    .Select(x => $"{ConstTitle} {x}")
                    .BindTo(this, x => x.Title)
                    .DisposeWith(disposables);
            });
        }

        public static string GenerateID(object result) => result.GetHashCode().ToString();
    }
}
