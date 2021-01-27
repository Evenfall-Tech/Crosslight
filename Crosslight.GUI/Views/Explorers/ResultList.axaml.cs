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
        public ListBox ResultTypeList => this.FindControl<ListBox>("resultTypeList");
        public ResultList()
        {
            Locator.CurrentMutable.Register(() => new ResultType(), typeof(IViewFor<ResultTypeVM>));
            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, x => x.ResultTypes, x => x.ResultTypeList.Items)
                    .DisposeWith(disp);
                //Observable
                //    .FromEventPattern<EventHandler<SelectionChangedEventArgs>, SelectionChangedEventArgs>
                //        (h => ResultListInter.SelectionChanged += h, h => ResultListInter.SelectionChanged -= h)
                //    .Subscribe(x =>
                //    {
                //        if (x != null && x.EventArgs != null)
                //        {
                //            ViewModel.SelectedIntermediateResults.RemoveMany(x.EventArgs.RemovedItems.OfType<ResultItemVM>());
                //            ViewModel.SelectedIntermediateResults.AddRange(x.EventArgs.AddedItems.OfType<ResultItemVM>());
                //        }
                //    })
                //    .DisposeWith(disp);
            });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
