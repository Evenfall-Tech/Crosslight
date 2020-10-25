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
    public class ResultListVM : ExplorerPanelVM, IActivatableViewModel
    {
        public new const string ConstTitle = "Result List";
        protected SourceCache<ResultItemVM, string> results;
        protected ReadOnlyObservableCollection<ResultItemVM> intermediateResults;
        protected IObservableCollection<ResultItemVM> selectedIntermediateResultsObservable;

        public ReadOnlyObservableCollection<ResultItemVM> IntermediateResults => intermediateResults;
        public IObservableCollection<ResultItemVM> SelectedIntermediateResults => selectedIntermediateResultsObservable;
        public ReactiveCommand<ResultItemVM, Unit> AddResultVM { get; }
        public ReactiveCommand<ResultItemVM, Unit> RemoveResultVM { get; }

        public override string Title => ConstTitle;
        public override string UrlPathSegment { get; } = "result_list";
        public ViewModelActivator Activator { get; }
        public ResultListVM() : this(null) { }
        public ResultListVM(IScreen screen) : base(screen)
        {
            results = new SourceCache<ResultItemVM, string>(x => x.Name);
            selectedIntermediateResultsObservable = new ObservableCollectionExtended<ResultItemVM>();

            AddResultVM = ReactiveCommand.Create((ResultItemVM item) =>
            {
                results.AddOrUpdate(item);
            }, Observable.Return(true));
            RemoveResultVM = ReactiveCommand.Create((ResultItemVM item) =>
            {
                results.Remove(item);
            }, Observable.Return(true));

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                results
                    .Connect()
                    .Filter(x => x.Origin == ResultItemOrigin.Intermediate)
                    .Bind(out intermediateResults)
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }
    }
}
