using Crosslight.API.Lang;
using ReactiveUI;
using System;
using System.Collections.Generic;
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
    }
}
