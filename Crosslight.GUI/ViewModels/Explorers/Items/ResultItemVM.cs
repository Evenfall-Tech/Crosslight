using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public enum ResultItemOrigin
    {
        Input,
        Intermediate,
        Output,
    }
    public class ResultItemVM : ReactiveObject, IActivatableViewModel
    {
        protected object result;
        protected string name;
        protected ResultItemOrigin origin;
        public object Result
        {
            get => result;
            set => this.RaiseAndSetIfChanged(ref result, value);
        }
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }
        public ResultItemOrigin Origin
        {
            get => origin;
            set => this.RaiseAndSetIfChanged(ref origin, value);
        }
        public ReactiveCommand<Unit, Unit> OpenCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

        public ViewModelActivator Activator { get; }
        public ResultItemVM()
        {
            OpenCommand = ReactiveCommand.Create(() =>
            {
                string id = ResultsVM.GenerateID(Result);
                var resultPanel = Locator.Current.GetService<ExplorerLocator>().Open<ResultsVM>(id: id, openExisting: true);
                if (resultPanel != null)
                {
                    resultPanel.Result = Result;
                }
            }, Observable.Return(true));
            RemoveCommand = ReactiveCommand.Create(() =>
            {
                var locator = Locator.Current.GetService<ExplorerLocator>();
                var props = locator.Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
                if (props != null)
                {
                    if (props.SelectedInstance == Result)
                        props.SelectedInstance = null;
                }
                var src = locator.Open<ResultListVM>();
                if (src != null)
                {
                    src.RemoveResultVM.Execute(this).Subscribe();
                }
                locator.Close((ExplorerPanelVM p) =>
                {
                    if (p is ResultsVM res)
                    {
                        if (res.Result == Result) return true;
                    }
                    return false;
                });
            }, Observable.Return(true));

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
            });
        }
    }
}
