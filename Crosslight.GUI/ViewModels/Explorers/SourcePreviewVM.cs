using Crosslight.API.IO;
using Crosslight.API.IO.FileSystem.Abstractions;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace Crosslight.GUI.ViewModels.Explorers
{
    public class SourcePreviewVM : ExplorerPanelVM, IActivatableViewModel
    {
        public new const string ConstTitle = "Source Preview";
        protected IFileSystemItem source;
        protected ObservableAsPropertyHelper<string> sourceText;
        protected ObservableAsPropertyHelper<string> title;
        protected ObservableAsPropertyHelper<string> idObservable;

        public IFileSystemItem Source
        {
            get => source;
            set => this.RaiseAndSetIfChanged(ref source, value);
        }
        public string SourceText => sourceText.Value;

        public override string Title => title.Value;
        public override string ID => idObservable.Value;
        public override string UrlPathSegment => $"source_preview_{title}";
        public ViewModelActivator Activator { get; }
        public SourcePreviewVM() : this(null) { }
        public SourcePreviewVM(IScreen screen) : base(screen)
        {
            var fileObservable = this
                .WhenAnyValue(x => x.Source)
                .DistinctUntilChanged();
            title = fileObservable
                .Select(x => TitleFromFile(x))
                .ToProperty(this, x => x.Title);
            idObservable = fileObservable
                .Select(x => IDFromFile(x))
                .ToProperty(this, x => x.ID);
            sourceText = fileObservable
                .Select(x => SourceTextFromFile(x))
                .ToProperty(this, x => x.SourceText);

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
            });
        }

        public static string GenerateID(IFileSystemItem source)
        {
            if (source == null) return null;
            if (source is IPhysicalFile fileSource)
            {
                return fileSource.Path.GetHashCode().ToString();
            }
            else if (source is IStringFile stringSource)
            {
                return stringSource.Text.GetHashCode().ToString();
            }
            else
                throw new NotImplementedException($"{source.GetType().Name} is not supported.");
        }

        private string TitleFromFile(IFileSystemItem src)
        {
            if (src == null) return null;
            if (src is IPhysicalFile fileSource)
            {
                return $"Src: {Path.GetFileName(fileSource.Path)}";
            }
            else if (src is IStringFile stringSource)
            {
                return "Src: string source";
            }
            else
                throw new NotImplementedException($"{src.GetType().Name} is not supported.");
        }

        private string SourceTextFromFile(IFileSystemItem src)
        {
            if (src == null) return null;
            if (src is IPhysicalFile fileSource)
            {
                return File.ReadAllText(fileSource.Path);
            }
            else if (src is IStringFile stringSource)
            {
                return stringSource.Text;
            }
            else
                throw new NotImplementedException($"{src.GetType().Name} is not supported.");
        }

        private string IDFromFile(IFileSystemItem src)
        {
            if (src == null) return null;
            return GenerateID(src);
        }
    }
}
