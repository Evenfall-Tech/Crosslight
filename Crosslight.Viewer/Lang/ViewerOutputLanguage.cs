using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.IO.FileSystem.Implementations;
using Crosslight.API.Lang;
using Crosslight.API.Nodes;
using Crosslight.Viewer.Nodes.Visitors;
using Crosslight.Viewer.ViewModels.Graph;
using Crosslight.Viewer.Views.Graph;
using System;

namespace Crosslight.Viewer.Lang
{
    public class ViewerOutputLanguage : OutputLanguage
    {
        public override string Name => "Viewer";

        private ViewerOptions options;
        public override LanguageConfig Config { get; protected set; }
        public override ILanguageOptions Options
        {
            get => options;
            set
            {
                options = value as ViewerOptions;
            }
        }
        public override void LoadOptionsFromConfig(LanguageConfig config)
        {
            throw new NotImplementedException();
        }

        public ViewerOutputLanguage()
        {
            options = new ViewerOptions();
        }

        public override IFileSystemItem Encode(Node rootNode)
        {
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
