using Crosslight.API.Transformers;
using DynamicData.Binding;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public class ResultTypeVM : ReactiveObject, IActivatableViewModel
    {
        protected TransformerType transformerType;
        protected ReadOnlyObservableCollection<ResultItemVM> results;
        protected ObservableCollectionExtended<ResultItemVM> selectedResultsObservable;

        public TransformerType TransformerType
        {
            get => transformerType;
            set => this.RaiseAndSetIfChanged(ref transformerType, value);
        }
        public ReadOnlyObservableCollection<ResultItemVM> Results
        {
            get => results;
            set => this.RaiseAndSetIfChanged(ref results, value);
        }
        public ObservableCollectionExtended<ResultItemVM> SelectedResults => selectedResultsObservable;
        public ViewModelActivator Activator { get; }
        public ResultTypeVM()
        {
            selectedResultsObservable = new ObservableCollectionExtended<ResultItemVM>();

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disp) =>
            {
            });
        }
    }
}
