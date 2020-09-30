using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels.Explorers.Items;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Crosslight.GUI.Views.Explorers.Items
{
    public class Language : ReactiveUserControl<LanguageVM>
    {
        public TextBlock Title => this.FindControl<TextBlock>("title");
        public Language()
        {
            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, x => x.Title, x => x.Title.Text)
                    .DisposeWith(disp);
            });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
