using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels.Explorers.Items;
using ReactiveUI;
using Splat;
using System.Reactive.Disposables;

namespace Crosslight.GUI.Views.Explorers.Items
{
    public class LanguageType : ReactiveUserControl<LanguageTypeVM>
    {
        public TextBlock LanguageName => this.FindControl<TextBlock>("name");
        public ListBox LanguageList => this.FindControl<ListBox>("list");
        public LanguageType()
        {
            Locator.CurrentMutable.Register(() => new Language(), typeof(IViewFor<LanguageVM>));

            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, x => x.Languages, x => x.LanguageList.Items)
                    .DisposeWith(disp);
                this.OneWayBind(ViewModel, x => x.LanguageType, x => x.LanguageName.Text)
                    .DisposeWith(disp);
                this.Bind(ViewModel, x => x.Selected, x => x.LanguageList.SelectedItem)
                    .DisposeWith(disp);
            });
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
