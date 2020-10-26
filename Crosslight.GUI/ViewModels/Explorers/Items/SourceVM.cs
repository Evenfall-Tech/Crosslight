﻿using Crosslight.API.IO;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers.Items
{
    public class SourceVM : ReactiveObject, IActivatableViewModel
    {
        protected Source source;
        protected string title;

        public Source Source
        {
            get => source;
            set => this.RaiseAndSetIfChanged(ref source, value);
        }
        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }
        public virtual IObservable<bool> SelectCommandAvailable { get; }
        public ReactiveCommand<Unit, Unit> SelectCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

        public ViewModelActivator Activator { get; }
        public SourceVM()
        {
            OpenCommand = ReactiveCommand.Create(() =>
            {
                string id = SourcePreviewVM.GenerateID(Source);
                var sourcePanel = Locator.Current.GetService<ExplorerLocator>().Open<SourcePreviewVM>(id: id, openExisting: true);
                if (sourcePanel != null)
                {
                    sourcePanel.Source = Source;
                }
            }, SelectCommandAvailable);
            SelectCommand = ReactiveCommand.Create(() =>
            {
                var props = Locator.Current.GetService<ExplorerLocator>().Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
                if (props != null)
                {
                    props.SelectedInstance = Source;
                }
            }, SelectCommandAvailable);
            RemoveCommand = ReactiveCommand.Create(() =>
            {
                var locator = Locator.Current.GetService<ExplorerLocator>();
                var props = locator.Open<PropertiesVM>(openExisting: true, createNewExplorer: false);
                if (props != null)
                {
                    if (props.SelectedInstance == Source)
                        props.SelectedInstance = null;
                }
                var src = locator.Open<SourceInputVM>();
                if (src != null)
                {
                    src.RemoveSource.Execute(this).Subscribe();
                }
                locator.Close((ExplorerPanelVM p) =>
                {
                    if (p is SourcePreviewVM prev) return prev.Source == Source;
                    return false;
                });
            }, SelectCommandAvailable);
            SelectCommandAvailable = this
                .WhenAnyValue(x => x.Source)
                .Select(k => k != null);

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
            });
        }

        public static SourceVM FromFile(string path)
        {
            var name = Path.GetFileName(path);
            var src = Source.FromFile(path);
            return new SourceVM()
            {
                title = name,
                source = src,
            };
        }
    }
}