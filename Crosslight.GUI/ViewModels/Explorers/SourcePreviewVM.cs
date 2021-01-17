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
        private string sourceText;
        private IFileSystemItem source;
        private string title;

        public string SourceText
        {
            get => sourceText;
            set => this.RaiseAndSetIfChanged(ref sourceText, value);
        }
        public IFileSystemItem Source
        {
            get => source;
            set => this.RaiseAndSetIfChanged(ref source, value);
        }
        public IObservable<string> VisibleSourceText { get; }

        public override string Title => $"Src: {title}";
        public override string UrlPathSegment => $"source_preview_{title}";
        public ViewModelActivator Activator { get; }
        public SourcePreviewVM() : this(null) { }
        public SourcePreviewVM(IScreen screen) : base(screen)
        {
            VisibleSourceText = this.WhenAnyValue(x => x.SourceText);

            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                this.WhenAnyValue(x => x.Source)
                    .DistinctUntilChanged()
                    .Select(x =>
                    {
                        SetTitle(x);
                        SetSourceText(x);
                        SetID(x);
                        return Unit.Default;
                    })
                    .Subscribe()
                    .DisposeWith(disposables);
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

        private Unit SetTitle(IFileSystemItem src)
        {
            if (src == null) return Unit.Default;
            if (src is IPhysicalFile fileSource)
            {
                title = Path.GetFileName(fileSource.Path);
                this.RaisePropertyChanged(nameof(Title));
            }
            else if (src is IStringFile stringSource)
            {
                title = "String Source";
                this.RaisePropertyChanged(nameof(Title));
            }
            else
                throw new NotImplementedException($"{src.GetType().Name} is not supported.");
            return Unit.Default;
        }

        private Unit SetSourceText(IFileSystemItem src)
        {
            if (src == null) return Unit.Default;
            if (src is IPhysicalFile fileSource)
            {
                SourceText = File.ReadAllText(fileSource.Path);
            }
            else if (src is IStringFile stringSource)
            {
                SourceText = stringSource.Text;
            }
            else
                throw new NotImplementedException($"{src.GetType().Name} is not supported.");
            return Unit.Default;
        }

        private Unit SetID(IFileSystemItem src)
        {
            ID = GenerateID(src);
            return Unit.Default;
        }
    }
}
