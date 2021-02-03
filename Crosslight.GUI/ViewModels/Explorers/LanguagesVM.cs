using Crosslight.API.Lang;
using Crosslight.Common.Runtime;
using Crosslight.GUI.ViewModels.Explorers.Items;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class LanguagesVM : ExplorerPanelVM, IActivatableViewModel
    {
        public new const string ConstTitle = "Languages";
        protected SourceCache<LanguageVM, string> languageSource;
        protected ObservableCollection<LanguageTypeVM> languageTypes;
        protected LanguageVM selectedLanguage;

        public ObservableCollection<LanguageTypeVM> LanguageTypes => languageTypes;
        public LanguageVM SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                if (value != null && value != selectedLanguage)
                {
                    this.RaiseAndSetIfChanged(ref selectedLanguage, value);
                    PropertiesVM properties = Locator.Current.GetService<IExplorerLocator>().Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
                    if (properties != null)
                        properties.SelectedInstance = selectedLanguage.Language.Options;
                }
            }
        }
        public ReactiveCommand<string, Unit> AddLanguage { get; }
        public ReactiveCommand<LanguageVM, Unit> RemoveLanguage { get; }

        public override string UrlPathSegment { get; } = "languages";
        public ViewModelActivator Activator { get; }
        public LanguagesVM() : this(null) { }
        public LanguagesVM(IScreen screen) : base(screen)
        {
            Title = ConstTitle;
            languageSource = new SourceCache<LanguageVM, string>(x => x.Title);
            AddLanguage = ReactiveCommand.Create((string s) =>
            {
                if (string.IsNullOrWhiteSpace(s)) return;
                var assembly = TypeLocator.LoadAssembly(s);
                if (assembly != null)
                {
                    Type[] languages = TypeLocator.FindTypesInAssembly<ILanguage>(assembly);
                    if (languages != null && languages.Length > 0)
                    {
                        foreach (var lang in languages)
                        {
                            ILanguage language = TypeLocator.CreateTypeInstance<ILanguage>(lang);
                            if (language != null)
                            {
                                var type = language.LanguageType;
                                LanguageVM vmToAdd = null;
                                vmToAdd = new LanguageVM()
                                {
                                    Path = s,
                                    Title = language.Name,
                                    Language = language,
                                };
                                if (vmToAdd != null)
                                {
                                    languageSource.AddOrUpdate(vmToAdd);
                                }
                            }
                        }
                    }
                }
            }, Observable.Return(true));
            RemoveLanguage = ReactiveCommand.Create((LanguageVM lang) =>
            {
                if (SelectedLanguage == lang)
                    SelectedLanguage = null;
                languageSource.Remove(lang);
            }, Observable.Return(true));

            languageTypes = new ObservableCollection<LanguageTypeVM>();
            var languateEnumValues = ((LanguageType[])Enum.GetValues(typeof(LanguageType))).Except(
                new LanguageType[]
                {
                    LanguageType.None,
                }
            );
            foreach (var languageType in languateEnumValues)
            {
                languageTypes.Add(new LanguageTypeVM()
                {
                    LanguageType = languageType,
                });
            }

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                var observable = languageSource.Connect();
                foreach (var languageType in languateEnumValues)
                {
                    var languageTypeVM = languageTypes.FirstOrDefault(x => x.LanguageType == languageType);
                    if (languageTypeVM != null)
                    {
                        observable
                            .Filter(x => x?.Language?.LanguageType == languageType)
                            .Bind(out var languagesObservable)
                            .Subscribe()
                            .DisposeWith(disposables);
                        languageTypeVM.Languages = languagesObservable;
                        this.WhenAnyValue(x => x.SelectedLanguage)
                            .DistinctUntilChanged()
                            .Do(x => Console.WriteLine(x?.Path))
                            .BindTo(languageTypeVM, x => x.Selected)
                            .DisposeWith(disposables);
                    }
                }
            });
        }
    }
}
