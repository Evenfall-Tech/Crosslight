using Crosslight.API.IO;
using Crosslight.GUI.ViewModels.Explorers.Items;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class SourceInputVM : ExplorerPanelVM, IActivatableViewModel
    {
        protected SourceCache<SourceVM, string> sources;
        protected ReadOnlyObservableCollection<SourceVM> fileSources;
        protected IObservableCollection<SourceVM> selectedSourcesObservable;

        public ReadOnlyObservableCollection<SourceVM> FileSources => fileSources;
        public IObservableCollection<SourceVM> SelectedSources => selectedSourcesObservable;
        public ReactiveCommand<SourceVM, Unit> AddSource { get; }
        public ReactiveCommand<SourceVM, Unit> RemoveSource { get; }

        public override string Title => "Source Input";
        public override string UrlPathSegment { get; } = "source_input";
        public ViewModelActivator Activator { get; }
        public SourceInputVM() : this(null) { }
        public SourceInputVM(IScreen screen) : base(screen)
        {
            sources = new SourceCache<SourceVM, string>(x => x.Title);
            selectedSourcesObservable = new ObservableCollectionExtended<SourceVM>();

            AddSource = ReactiveCommand.Create((SourceVM src) =>
            {
                sources.AddOrUpdate(src);
            }, Observable.Return(true));
            RemoveSource = ReactiveCommand.Create((SourceVM src) =>
            {
                sources.Remove(src);
            }, Observable.Return(true));

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                sources
                    .Connect()
                    .Filter(x => x.Source is MultiFileSource)
                    .Bind(out fileSources)
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }
    }
}
