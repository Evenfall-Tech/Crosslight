using Crosslight.API.IO;
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
        private Source source;
        private string title;

        public string SourceText
        {
            get => sourceText;
            set => this.RaiseAndSetIfChanged(ref sourceText, value);
        }
        public Source Source
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

        public static string GenerateID(Source source)
        {
            if (source == null) return null;
            if (source is MultiFileSource fileSource)
            {
                if (fileSource.Count != 1)
                    throw new NotImplementedException($"More than one file in {nameof(MultiFileSource)} is not supported.");
                return fileSource.Files.First().GetHashCode().ToString();
            }
            else if (source is MultiStringSource stringSource)
            {
                if (stringSource.Count != 1)
                    throw new NotImplementedException($"More than one string in {nameof(MultiStringSource)} is not supported.");
                return stringSource.Strings.First().GetHashCode().ToString();
            }
            else
                throw new NotImplementedException($"{source.GetType().Name} is not supported.");
        }

        private Unit SetTitle(Source src)
        {
            if (src == null) return Unit.Default;
            if (src is MultiFileSource fileSource)
            {
                if (fileSource.Count != 1)
                    throw new NotImplementedException($"More than one file in {nameof(MultiFileSource)} is not supported.");
                title = Path.GetFileName(fileSource.Files.First());
                this.RaisePropertyChanged(nameof(Title));
            }
            else if (src is MultiStringSource stringSource)
            {
                if (stringSource.Count != 1)
                    throw new NotImplementedException($"More than one string in {nameof(MultiStringSource)} is not supported.");
                title = "String Source";
                this.RaisePropertyChanged(nameof(Title));
            }
            else
                throw new NotImplementedException($"{src.GetType().Name} is not supported.");
            return Unit.Default;
        }

        private Unit SetSourceText(Source src)
        {
            if (src == null) return Unit.Default;
            if (src is MultiFileSource fileSource)
            {
                if (fileSource.Count != 1)
                    throw new NotImplementedException($"More than one file in {nameof(MultiFileSource)} is not supported.");
                SourceText = File.ReadAllText(fileSource.Files.First());
            }
            else if (src is MultiStringSource stringSource)
            {
                if (stringSource.Count != 1)
                    throw new NotImplementedException($"More than one string in {nameof(MultiStringSource)} is not supported.");
                SourceText = stringSource.Strings.First();
            }
            else
                throw new NotImplementedException($"{src.GetType().Name} is not supported.");
            return Unit.Default;
        }

        private Unit SetID(Source src)
        {
            ID = GenerateID(src);
            return Unit.Default;
        }
    }
}
