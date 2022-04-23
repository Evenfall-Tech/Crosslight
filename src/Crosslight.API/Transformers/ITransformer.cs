using Crosslight.API.IO.FileSystem.Abstractions;

namespace Crosslight.API.Transformers
{
    public interface ITransformer
    {
        IFileSystemItem Translate(IFileSystemItem source);
        string Name { get; }
        TransformerType TransformerType { get; }
        TransformerConfig Config { get; }
        ITransformerOptions Options { get; set; }
        /// <summary>
        /// Set transformer config and options based on the parameter.
        /// </summary>
        /// <param name="config">Transformer config to load options from.</param>
        void LoadOptionsFromConfig(TransformerConfig config);
    }
}
