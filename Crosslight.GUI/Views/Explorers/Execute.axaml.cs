using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.API.IO.FileSystem.Implementations;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.ViewModels.Explorers.Items;
using ReactiveUI;
using Splat;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Crosslight.GUI.Views.Explorers
{
    public class Execute : ReactiveUserControl<ExecuteVM>
    {
        public Button Decode => this.FindControl<Button>("decode");
        public Button Encode => this.FindControl<Button>("encode");
        ReactiveCommand<Unit, Unit> DecodeCommand => ReactiveCommand.CreateFromTask(ExecuteDecode);
        ReactiveCommand<Unit, Unit> EncodeCommand => ReactiveCommand.CreateFromTask(ExecuteEncode);
        public Execute()
        {
            this.WhenActivated(disp =>
            {
                this.WhenAnyValue(x => x.DecodeCommand)
                    .BindTo(this, x => x.Decode.Command)
                    .DisposeWith(disp);
                this.WhenAnyValue(x => x.EncodeCommand)
                    .BindTo(this, x => x.Encode.Command)
                    .DisposeWith(disp);
            });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async Task ExecuteDecode()
        {
            var node = await ViewModel.Decode.Execute();
            var resultList = Locator.Current.GetService<ExplorerLocator>().Open<ResultListVM>(openExisting: true);
            if (resultList != null)
                await resultList.AddResultVM.Execute(new ResultItemVM()
                {
                    Name = node.ToString(),
                    Origin = ResultItemOrigin.Intermediate,
                    Result = node,
                });
            string id = ResultsVM.GenerateID(node);
            var resultPanel = Locator.Current.GetService<ExplorerLocator>().Open<ResultsVM>(id: id, openExisting: true);
            if (resultPanel != null)
            {
                resultPanel.Result = new CustomFile("Node", node);
            }
        }

        private async Task ExecuteEncode()
        {
            var result = await ViewModel.Encode.Execute();
            string id = ResultsVM.GenerateID(result);
            var resultList = Locator.Current.GetService<ExplorerLocator>().Open<ResultListVM>(openExisting: true);
            if (resultList != null)
                await resultList.AddResultVM.Execute(new ResultItemVM()
                {
                    Name = $"Result {id}",
                    Origin = ResultItemOrigin.Output,
                    Result = result,
                });
            var resultPanel = Locator.Current.GetService<ExplorerLocator>().Open<ResultsVM>(id: id, openExisting: true);
            if (resultPanel != null)
            {
                resultPanel.Result = result;
            }
        }
    }
}
