using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.IO.FileSystem.Implementations;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.Language.Viewer.Nodes.Visitors;
using Crosslight.Language.Viewer.ViewModels.Graph;
using Crosslight.Language.Viewer.Views.Graph;
using System;

namespace Crosslight.Language.Viewer.Lang
{
    public class ViewerOutputLanguage : ILanguage
    {
        public string Name => "Viewer";
        public LanguageType LanguageType => LanguageType.Output;

        private ViewerOptions options;
        public LanguageConfig Config { get; protected set; }
        public ILanguageOptions Options
        {
            get => options;
            set
            {
                options = value as ViewerOptions;
            }
        }
        public void LoadOptionsFromConfig(LanguageConfig config)
        {
            throw new NotImplementedException();
        }

        public ViewerOutputLanguage()
        {
            options = new ViewerOptions();
        }

        public IFileSystemItem Translate(IFileSystemItem input)
        {
            Node rootNode = (input as IFile).Content as Node;
            if (options.LaunchApplication)
                return new CustomFile(
                    "Return Code",
                    Program.LaunchApplication(
                        new Avalonia.ApplicationOptions()
                        {
                            Options = options,
                            RootNode = rootNode,
                        }
                    )
                );
            return new CustomFile(
                "Graph Viewer",
                new GraphViewer()
                {
                    ViewModel = new GraphViewerViewModel()
                    {
                        RootNode = rootNode,
                    }
                }
            );
        }
    }
}
