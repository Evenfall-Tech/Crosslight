using Crosslight.API.Lang;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public class LanguageTypeVM : ReactiveObject, IActivatableViewModel
    {
        protected LanguageType languageType;
        protected ReadOnlyObservableCollection<LanguageVM> languages;
        protected LanguageVM selected;

        public LanguageType LanguageType
        {
            get => languageType;
            set => this.RaiseAndSetIfChanged(ref languageType, value);
        }
        public ReadOnlyObservableCollection<LanguageVM> Languages
        {
            get => languages;
            set => this.RaiseAndSetIfChanged(ref languages, value);
        }
        public LanguageVM Selected
        {
            get => selected;
            set => this.RaiseAndSetIfChanged(ref selected, value);
        }

        public ViewModelActivator Activator { get; }
        public LanguageTypeVM()
        {
            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disp) =>
            {
            });
        }
    }
}
