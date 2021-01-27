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
    public class ResultType : ReactiveUserControl<ResultTypeVM>
    {
        public TextBlock ResultName => this.FindControl<TextBlock>("name");
        public ListBox ResultItemList => this.FindControl<ListBox>("list");
        public ResultType()
        {
            Locator.CurrentMutable.Register(() => new ResultItem(), typeof(IViewFor<ResultItemVM>));

            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, x => x.Results, x => x.ResultItemList.Items)
                    .DisposeWith(disp);
                this.OneWayBind(ViewModel, x => x.LanguageType, x => x.ResultName.Text)
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
