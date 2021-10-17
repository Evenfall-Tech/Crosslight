using Crosslight.API.Lang;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public class LanguageVM : ReactiveObject
    {
        protected string path;
        protected string title;
        protected ILanguage language;
        public ILanguage Language
        {
            get => language;
            set => this.RaiseAndSetIfChanged(ref language, value);
        }
        public string Path
        {
            get => path;
            set => this.RaiseAndSetIfChanged(ref path, value);
        }
        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }
        protected IObservable<bool> SelectCommandAvailable => this
            .WhenAnyValue(x => x.Language, x => x.Path, (language, path) => language != null && !string.IsNullOrWhiteSpace(path));
        public ReactiveCommand<Unit, Unit> SelectCommand => ReactiveCommand.Create(() =>
        {
            var locator = Locator.Current.GetService<IExplorerLocator>();
            var props = locator.Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
            if (props != null)
            {
                props.SelectedInstance = Language.Options;
            }
            var lang = locator.Open<LanguagesVM>();
            if (lang != null)
            {
                lang.SelectedLanguage = this;
            }
        },
        SelectCommandAvailable);
        public ReactiveCommand<Unit, Unit> RemoveCommand => ReactiveCommand.Create(() =>
        {
            var locator = Locator.Current.GetService<IExplorerLocator>();
            var props = locator.Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
            if (props != null)
            {
                if (props.SelectedInstance == Language.Options)
                    props.SelectedInstance = null;
            }
            var lang = locator.Open<LanguagesVM>();
            if (lang != null)
            {
                lang.RemoveLanguage.Execute(this).Subscribe();
            }
        },
        SelectCommandAvailable);
    }
}
