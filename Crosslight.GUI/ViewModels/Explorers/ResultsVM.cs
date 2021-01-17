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

        public IFileSystemItem Result
        {
            get => result;
            set => this.RaiseAndSetIfChanged(ref result, value);
        }

        public override string Title => $"{ConstTitle} {ID}";
        public override string UrlPathSegment => $"result_{ID}";
        public ViewModelActivator Activator { get; }
        public ResultsVM() : this(null) { }
        public ResultsVM(IScreen screen) : base(screen)
        {

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                this.WhenAnyValue(x => x.Result)
                    .DistinctUntilChanged()
                    .Select(x =>
                    {
                        if (x != null)
                        {
                            ID = GenerateID(x);
                            this.RaisePropertyChanged(nameof(Title));
                        }
                        return Unit.Default;
                    })
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }

        public static string GenerateID(object result) => result.GetHashCode().ToString();
    }
}
