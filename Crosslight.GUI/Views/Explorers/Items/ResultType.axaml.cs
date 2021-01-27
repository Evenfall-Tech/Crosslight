using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels.Explorers.Items;
using DynamicData;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

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
                Observable
                    .FromEventPattern<EventHandler<SelectionChangedEventArgs>, SelectionChangedEventArgs>
                        (h => ResultItemList.SelectionChanged += h, h => ResultItemList.SelectionChanged -= h)
                    .Subscribe(x =>
                    {
                        if (x != null && x.EventArgs != null)
                        {
                            using (ViewModel.SelectedResults.SuspendNotifications())
                            {
                                ViewModel.SelectedResults.RemoveMany(x.EventArgs.RemovedItems.OfType<ResultItemVM>());
                                ViewModel.SelectedResults.AddRange(x.EventArgs.AddedItems.OfType<ResultItemVM>());
                            }
                        }
                    })
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
