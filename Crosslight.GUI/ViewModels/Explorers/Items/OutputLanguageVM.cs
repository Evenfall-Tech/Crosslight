using Crosslight.API.Lang;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public class OutputLanguageVM : LanguageVM
    {
        protected OutputLanguage outputLanguage;
        public OutputLanguage OutputLanguage
        {
            get => outputLanguage;
            set => this.RaiseAndSetIfChanged(ref outputLanguage, value);
        }
    }
}
