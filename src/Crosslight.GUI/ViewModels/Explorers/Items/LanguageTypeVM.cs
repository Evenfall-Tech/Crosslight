using Crosslight.API.Transformers;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public class TransformerTypeVM : ReactiveObject, IActivatableViewModel
    {
        protected TransformerType transformerType;
        protected ReadOnlyObservableCollection<TransformerVM> transformers;
        protected TransformerVM selected;

        public TransformerType TransformerType
        {
            get => transformerType;
            set => this.RaiseAndSetIfChanged(ref transformerType, value);
        }
        public ReadOnlyObservableCollection<TransformerVM> Transformers
        {
            get => transformers;
            set => this.RaiseAndSetIfChanged(ref transformers, value);
        }
        public TransformerVM Selected
        {
            get => selected;
            set => this.RaiseAndSetIfChanged(ref selected, value);
        }

        public ViewModelActivator Activator { get; }
        public TransformerTypeVM()
        {
            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disp) =>
            {
            });
        }
    }
}
