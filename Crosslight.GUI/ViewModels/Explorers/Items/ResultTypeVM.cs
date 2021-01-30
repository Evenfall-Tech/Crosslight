using Crosslight.API.Lang;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public class ResultTypeVM : ReactiveObject, IActivatableViewModel
    {
        protected LanguageType languageType;
        protected ReadOnlyObservableCollection<ResultItemVM> results;
        protected ObservableCollectionExtended<ResultItemVM> selectedResultsObservable;

        public LanguageType LanguageType
        {
            get => languageType;
            set => this.RaiseAndSetIfChanged(ref languageType, value);
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
