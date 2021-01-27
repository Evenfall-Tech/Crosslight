using Crosslight.API.Lang;
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
        protected IObservableCollection<ResultItemVM> selectedResultsObservable;

        public ObservableCollection<ResultTypeVM> ResultTypes => resultTypes;
        public IObservableCollection<ResultItemVM> SelectedResults => selectedResultsObservable;
        public ReactiveCommand<ResultItemVM, Unit> AddResultVM { get; }
        public ReactiveCommand<ResultItemVM, Unit> RemoveResultVM { get; }

        public override string Title => ConstTitle;
        public override string UrlPathSegment { get; } = "result_list";
        public ViewModelActivator Activator { get; }
        public ResultListVM() : this(null) { }
        public ResultListVM(IScreen screen) : base(screen)
        {
            resultsSource = new SourceCache<ResultItemVM, string>(x => x.Name);
            selectedResultsObservable = new ObservableCollectionExtended<ResultItemVM>();

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
            var languateEnumValues = (LanguageType[])Enum.GetValues(typeof(LanguageType));
            foreach (var languageType in languateEnumValues)
            {
                resultTypes.Add(new ResultTypeVM()
                {
                    LanguageType = languageType,
                });
            }

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                var observable = resultsSource.Connect();
                foreach (var languageType in languateEnumValues)
                {
                    var resultTypeVM = resultTypes.FirstOrDefault(x => x.LanguageType == languageType);
                    if (resultTypeVM != null)
                    {
                        observable
                            .Filter(x => x?.Origin == languageType)
                            .Bind(out var resultsObservable)
                            .Subscribe()
                            .DisposeWith(disposables);
                        resultTypeVM.Results = resultsObservable;
                    }
                }
            });
        }
    }
}
