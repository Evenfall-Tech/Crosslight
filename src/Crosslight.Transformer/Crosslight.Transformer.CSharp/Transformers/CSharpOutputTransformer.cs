using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.Transformers;
using Crosslight.Transformer.CSharp.Nodes.Visitors;
using System;

namespace Crosslight.Transformer.CSharp.Transformers
{
    public class CSharpOutputTransformer : ITransformer
    {
        public string Name => "CSharp";
        public TransformerType TransformerType => TransformerType.Output;

        private CSharpVisitOptions options;
        public TransformerConfig Config { get; protected set; }
        public ITransformerOptions Options
        {
            get => options;
            set
            {
                options = value as CSharpVisitOptions;
            }
        }
        public void LoadOptionsFromConfig(TransformerConfig config)
        {
            throw new NotImplementedException();
        }

        public IFileSystemItem Translate(IFileSystemItem rootNode)
        {
            throw new NotImplementedException();
        }

        public CSharpOutputTransformer()
        {
            options = new CSharpVisitOptions();
        }
    }
}
