using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.IO.FileSystem.Implementations;
using Crosslight.API.Transformers;
using Crosslight.API.Nodes.Implementations;
using Crosslight.Transformer.Viewer.Nodes.Visitors;
using Crosslight.Transformer.Viewer.ViewModels.Graph;
using Crosslight.Transformer.Viewer.Views.Graph;
using System;

namespace Crosslight.Transformer.Viewer.Transformers
{
    public class ViewerOutputTransformer : ITransformer
    {
        public string Name => "Viewer";
        public TransformerType TransformerType => TransformerType.Output;

        private ViewerOptions options;
        public TransformerConfig Config { get; protected set; }
        public ITransformerOptions Options
        {
            get => options;
            set
            {
                options = value as ViewerOptions;
            }
        }
        public void LoadOptionsFromConfig(TransformerConfig config)
        {
            throw new NotImplementedException();
        }

        public ViewerOutputTransformer()
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
