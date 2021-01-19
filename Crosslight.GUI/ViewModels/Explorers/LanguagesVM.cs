using Crosslight.API.Lang;
using Crosslight.Common.Runtime;
using Crosslight.GUI.ViewModels.Explorers.Items;
using DynamicData;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class LanguagesVM : ExplorerPanelVM, IActivatableViewModel
    {
        public new const string ConstTitle = "Languages";
        protected SourceCache<LanguageVM, string> languageSource;
        protected ReadOnlyObservableCollection<InputLanguageVM> inputLanguages;
        protected InputLanguageVM selectedInputLanguage;
        protected ReadOnlyObservableCollection<OutputLanguageVM> outputLanguages;
        protected OutputLanguageVM selectedOutputLanguage;

        public ReadOnlyObservableCollection<InputLanguageVM> InputLanguages => inputLanguages;
        public ReadOnlyObservableCollection<OutputLanguageVM> OutputLanguages => outputLanguages;
        public InputLanguageVM SelectedInputLanguage
        {
            get => selectedInputLanguage;
            set
            {
                if (value != null && value != selectedInputLanguage)
                {
                    this.RaiseAndSetIfChanged(ref selectedInputLanguage, value);
                    PropertiesVM properties = Locator.Current.GetService<ExplorerLocator>().Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
                    if (properties != null)
                        properties.SelectedInstance = selectedInputLanguage.InputLanguage.Options;
                }
            }
        }
        public OutputLanguageVM SelectedOutputLanguage
        {
            get => selectedOutputLanguage;
            set
            {
                if (value != null && value != selectedOutputLanguage)
                {
                    this.RaiseAndSetIfChanged(ref selectedOutputLanguage, value);
                    PropertiesVM properties = Locator.Current.GetService<ExplorerLocator>().Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
                    if (properties != null)
                        properties.SelectedInstance = selectedOutputLanguage.OutputLanguage.Options;
                }
            }
        }
        public ReactiveCommand<string, Unit> AddLanguage { get; }
        public ReactiveCommand<LanguageVM, Unit> RemoveLanguage { get; }

        public override string Title => ConstTitle;
        public override string UrlPathSegment { get; } = "languages";
        public ViewModelActivator Activator { get; }
        public LanguagesVM() : this(null) { }
        public LanguagesVM(IScreen screen) : base(screen)
        {
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
                            // TODO: refactor language selection to support any number of LanguageTypes.
                            ILanguage language = TypeLocator.CreateTypeInstance<ILanguage>(lang);
                            LanguageVM vmToAdd = null;
                            vmToAdd = language.LanguageType switch
                            {
                                LanguageType.Input => new InputLanguageVM()
                                {
                                    Path = s,
                                    Title = language.Name,
                                    InputLanguage = language,
                                },
                                LanguageType.Output => new OutputLanguageVM()
                                {
                                    Path = s,
                                    Title = language.Name,
                                    OutputLanguage = language,
                                },
                                _ => throw new NotImplementedException(),
                            };
                            if (vmToAdd != null)
                                languageSource.AddOrUpdate(vmToAdd);
                        }
                    }
                }
            }, Observable.Return(true));
            RemoveLanguage = ReactiveCommand.Create((LanguageVM lang) =>
            {
                if (lang is InputLanguageVM inp)
                {
                    if (SelectedInputLanguage == inp)
                        SelectedInputLanguage = null;
                }
                else if (lang is OutputLanguageVM oup)
                {
                    if (SelectedOutputLanguage == oup)
                        SelectedOutputLanguage = null;
                }
                else throw new NotImplementedException();
                languageSource.Remove(lang);
            }, Observable.Return(true));

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                var observable = languageSource.Connect();
                observable
                    .Filter(x => x is InputLanguageVM)
                    .Transform(x => x as InputLanguageVM)
                    .Bind(out inputLanguages)
                    .Subscribe()
                    .DisposeWith(disposables);
                observable
                    .Filter(x => x is OutputLanguageVM)
                    .Transform(x => x as OutputLanguageVM)
                    .Bind(out outputLanguages)
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }
    }
}
