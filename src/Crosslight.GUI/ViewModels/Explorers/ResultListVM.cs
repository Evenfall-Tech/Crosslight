using Crosslight.API.Transformers;
using Crosslight.GUI.ViewModels.Explorers.Items;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class ResultListVM : ExplorerPanelVM, IActivatableViewModel
    {
        public new const string ConstTitle = "Result List";
        protected SourceCache<ResultItemVM, string> resultsSource;
        protected ObservableCollection<ResultTypeVM> resultTypes;
        protected ReadOnlyObservableCollection<ResultItemVM> selectedResultsObservable;

        public ObservableCollection<ResultTypeVM> ResultTypes => resultTypes;
        public ReadOnlyObservableCollection<ResultItemVM> SelectedResults => selectedResultsObservable;
        public ReactiveCommand<ResultItemVM, Unit> AddResultVM { get; }
        public ReactiveCommand<ResultItemVM, Unit> RemoveResultVM { get; }

        public override string UrlPathSegment { get; } = "result_list";
        public ViewModelActivator Activator { get; }
        public ResultListVM() : this(null) { }
        public ResultListVM(IScreen screen) : base(screen)
        {
            Title = ConstTitle;
            resultsSource = new SourceCache<ResultItemVM, string>(x => x.ID);

            AddResultVM = ReactiveCommand.Create((ResultItemVM item) =>
            {
                if (item != null)
                    resultsSource.AddOrUpdate(item);
            }, Observable.Return(true));
            RemoveResultVM = ReactiveCommand.Create((ResultItemVM item) =>
            {
                if (item != null)
                    resultsSource.Remove(item);
            }, Observable.Return(true));

            resultTypes = new ObservableCollection<ResultTypeVM>();
            var transformerEnumValues = ((TransformerType[])Enum.GetValues(typeof(TransformerType))).Except(
                new TransformerType[]
                {
                    TransformerType.None,
                }
            );
            foreach (var transformerType in transformerEnumValues)
            {
                resultTypes.Add(new ResultTypeVM()
                {
                    TransformerType = transformerType,
                });
            }

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                foreach (var transformerType in transformerEnumValues)
                {
                    var resultTypeVM = resultTypes.FirstOrDefault(x => x.TransformerType == transformerType);
                    if (resultTypeVM != null)
                    {
                        resultsSource
                            .Connect()
                            .Filter(x => x?.Origin == transformerType)
                            .Bind(out var resultsObservable)
                            .Subscribe()
                            .DisposeWith(disposables);
                        resultTypeVM.Results = resultsObservable;
                    }
                }
                // TODO: this code can probably be refactored without two change sets.
                Observable
                    .Merge(ResultTypes.Select(x => x.SelectedResults.ToObservableChangeSet()))
                    .Bind(out selectedResultsObservable)
                    .Do(x => Console.WriteLine(x?.Count))
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }
    }
}
