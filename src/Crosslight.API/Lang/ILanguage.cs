using Crosslight.API.IO.FileSystem.Abstractions;

namespace Crosslight.API.Lang
{
    public interface ILanguage
    {
        IFileSystemItem Translate(IFileSystemItem source);
        string Name { get; }
        LanguageType LanguageType { get; }
        LanguageConfig Config { get; }
        ILanguageOptions Options { get; set; }
        /// <summary>
        /// Set language config and options based on the parameter.
        /// </summary>
        /// <param name="config">Language config to load options from.</param>
        void LoadOptionsFromConfig(LanguageConfig config);
    }
}
