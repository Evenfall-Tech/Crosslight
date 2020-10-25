using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Explorers.Items;
using Crosslight.GUI.Views.Explorers.Items;
using DynamicData;
using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.Views.Explorers
{
    public class ResultList : ReactiveUserControl<ResultListVM>
    {
        public ListBox ResultListInput => this.FindControl<ListBox>("resultListInput");
        public ListBox ResultListInter => this.FindControl<ListBox>("resultListInter");
        public ListBox ResultListOutput => this.FindControl<ListBox>("resultListOutput");
        public ResultList()
        {
            Locator.CurrentMutable.Register(() => new ResultItem(), typeof(IViewFor<ResultItemVM>));
            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, x => x.IntermediateResults, x => x.ResultListInter.Items)
                    .DisposeWith(disp);
                Observable
                    .FromEventPattern<EventHandler<SelectionChangedEventArgs>, SelectionChangedEventArgs>
                        (h => ResultListInter.SelectionChanged += h, h => ResultListInter.SelectionChanged -= h)
                    .Subscribe(x =>
                    {
                        if (x != null && x.EventArgs != null)
                        {
                            ViewModel.SelectedIntermediateResults.RemoveMany(x.EventArgs.RemovedItems.OfType<ResultItemVM>());
                            ViewModel.SelectedIntermediateResults.AddRange(x.EventArgs.AddedItems.OfType<ResultItemVM>());
                        }
                    })
                    .DisposeWith(disp);
                ResultListInter
                    .WhenAnyValue(x => x.SelectedItem)
                    .Where(x => x is ResultItemVM)
                    .Select(x => x as ResultItemVM)
                    //.Select(x => x.SelectCommand.Execute())
                    .Subscribe()
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
