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
    public class InputLanguageVM : LanguageVM
    {
        protected InputLanguage inputLanguage;
        public InputLanguage InputLanguage
        {
            get => inputLanguage;
            set => this.RaiseAndSetIfChanged(ref inputLanguage, value);
        }

        public override ReactiveCommand<Unit, Unit> SelectCommand => ReactiveCommand.Create(() =>
        {
            var props = Locator.Current.GetService<PropertiesVM>();
            if (props != null)
            {
                props.SelectedInstance = InputLanguage.Options;
            }
            var lang = Locator.Current.GetService<LanguagesVM>();
            if (lang != null)
            {
                lang.SelectedInputLanguage = this;
            }
        },
        SelectCommandAvailable);

        protected override IObservable<bool> SelectCommandAvailable => this
            .WhenAnyValue(x => x.InputLanguage, x => x.Path, x => x.InputLanguage.Options)
            .Select(k => true/*k.Item1 != null && !string.IsNullOrWhiteSpace(k.Item2) && k.Item3 != null*/);
    }
}
