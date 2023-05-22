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
    public class TransformerType : ReactiveUserControl<TransformerTypeVM>
    {
        public TextBlock TransformerName => this.FindControl<TextBlock>("name");
        public ListBox TransformerList => this.FindControl<ListBox>("list");
        public TransformerType()
        {
            Locator.CurrentMutable.Register(() => new Transformer(), typeof(IViewFor<TransformerVM>));

            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, x => x.Transformers, x => x.TransformerList.Items)
                    .DisposeWith(disp);
                this.OneWayBind(ViewModel, x => x.TransformerType, x => x.TransformerName.Text)
                    .DisposeWith(disp);
                this.Bind(ViewModel, x => x.Selected, x => x.TransformerList.SelectedItem)
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
