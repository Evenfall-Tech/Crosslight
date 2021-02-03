using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Crosslight.API.IO.FileSystem.Implementations;
using Crosslight.GUI.ViewModels;
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
        public Button Translate => this.FindControl<Button>("translate");
        ReactiveCommand<Unit, Unit> TranslateCommand => ReactiveCommand.CreateFromTask(ExecuteTranslate);
        public Execute()
        {
            this.WhenActivated(disp =>
            {
                this.WhenAnyValue(x => x.TranslateCommand)
                    .BindTo(this, x => x.Translate.Command)
                    .DisposeWith(disp);
            });
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async Task ExecuteTranslate()
        {
            var translationResult = await ViewModel.Translate.Execute();
            if (translationResult.language != null && translationResult.result != null)
            {
                var resultList = Locator.Current.GetService<IExplorerLocator>().Open<ResultListVM>(openExisting: true);
                if (resultList != null)
                    await resultList.AddResultVM.Execute(new ResultItemVM()
                    {
                        Name = translationResult.result.Name,
                        Origin = translationResult.language.LanguageType,
                        Result = translationResult.result,
                    });
                string id = ResultsVM.GenerateID(translationResult.result);
                var resultPanel = Locator.Current.GetService<IExplorerLocator>().Open<ResultsVM>(id: id, openExisting: true);
                if (resultPanel != null)
                {
                    resultPanel.Result = translationResult.result;
                }
            }
        }
    }
}
