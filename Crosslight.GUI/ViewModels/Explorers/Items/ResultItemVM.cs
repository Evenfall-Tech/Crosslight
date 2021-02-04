using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.Lang;
using ReactiveUI;
using Splat;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public enum ResultItemState
    {
        None = 0,
        NonExpandable,
        Collapsed,
        Expanded
    }
    public class ResultItemVM : ReactiveObject, IActivatableViewModel
    {
        protected IFileSystemItem result;
        protected string name;
        protected LanguageType origin;
        protected ObservableAsPropertyHelper<string> id;
        protected ResultItemState state;
        protected bool isTopLevel;
        public ResultItemState State
        {
            get => state;
            set => this.RaiseAndSetIfChanged(ref state, value);
        }
        public IFileSystemItem Result
        {
            get => result;
            set => this.RaiseAndSetIfChanged(ref result, value);
        }
        public string ID => id.Value;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }
        public bool IsTopLevel
        {
            get => isTopLevel;
            set => this.RaiseAndSetIfChanged(ref isTopLevel, value);
        }
        public LanguageType Origin
        {
            get => origin;
            set => this.RaiseAndSetIfChanged(ref origin, value);
        }
        public ReactiveCommand<Unit, Unit> OpenCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

        public ViewModelActivator Activator { get; }
        public ResultItemVM()
        {
            IsTopLevel = true;

            OpenCommand = ReactiveCommand.Create(() =>
            {
                if (Result is IDirectory)
                {
                    State = SwitchState(State);
                }
                else if (Result is IFile)
                {
                    string id = ResultsVM.GenerateID(Result);
                    var resultPanel = Locator.Current.GetService<IExplorerLocator>().Open<ResultsVM>(id: id, openExisting: true);
                    if (resultPanel != null)
                    {
                        resultPanel.Result = Result;
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }, Observable.Return(true));
            RemoveCommand = ReactiveCommand.Create(() =>
            {
                var locator = Locator.Current.GetService<IExplorerLocator>();
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

            id = this
                .WhenAnyValue(x => x.Result)
                .DistinctUntilChanged()
                .Where(x => x != null)
                .Select(x => x?.GetHashCode().ToString())
                .ToProperty(this, x => x.ID, this.GetHashCode().ToString());

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disp) =>
            {
                this.WhenAnyValue(x => x.Result)
                    .Select(x => StateFromFile(x))
                    .BindTo(this, x => x.State)
                    .DisposeWith(disp);
            });
        }

        public static ResultItemState SwitchState(ResultItemState state)
        {
            return state switch
            {
                ResultItemState.Collapsed => ResultItemState.Expanded,
                ResultItemState.Expanded => ResultItemState.Collapsed,
                ResultItemState.NonExpandable => ResultItemState.NonExpandable,
                _ => ResultItemState.None,
            };
        }

        private ResultItemState StateFromFile(IFileSystemItem file)
        {
            if (file is null) throw new NullReferenceException();
            if (file is IDirectory) return ResultItemState.Collapsed;
            if (file is IFile) return ResultItemState.NonExpandable;
            else throw new NotImplementedException($"{file.GetType().Name} file is not yet supported.");
        }
    }
}
