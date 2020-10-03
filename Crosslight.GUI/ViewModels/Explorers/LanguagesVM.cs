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
        public static LanguagesVM Instance { get; protected set; }

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
                    Locator.Current.GetService<PropertiesVM>().SelectedInstance = selectedInputLanguage.InputLanguage.Options;
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
                    Locator.Current.GetService<PropertiesVM>().SelectedInstance = selectedOutputLanguage.OutputLanguage.Options;
                }
            }
        }
        public ReactiveCommand<string, Unit> AddLanguage { get; }
        public ReactiveCommand<LanguageVM, Unit> RemoveLanguage { get; }

        public override string Title => "Languages";
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
                    Type[] inp = TypeLocator.FindTypesInAssembly<InputLanguage>(assembly);
                    Type[] oup = TypeLocator.FindTypesInAssembly<OutputLanguage>(assembly);
                    if (inp != null && inp.Length > 0)
                    {
                        foreach (var item in inp)
                        {
                            InputLanguage inputLanguage = TypeLocator.CreateTypeInstance<InputLanguage>(item);
                            languageSource.AddOrUpdate(new InputLanguageVM()
                            {
                                Path = s,
                                Title = inputLanguage.Name,
                                InputLanguage = inputLanguage,
                            });
                        }
                    }
                    if (oup != null && oup.Length > 0)
                    {
                        foreach (var item in oup)
                        {
                            OutputLanguage outputLanguage = TypeLocator.CreateTypeInstance<OutputLanguage>(item);
                            languageSource.AddOrUpdate(new OutputLanguageVM()
                            {
                                Path = s,
                                Title = outputLanguage.Name,
                                OutputLanguage = outputLanguage,
                            });
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

            if (Instance == null) Instance = this;
        }
    }
}
