using Crosslight.API.Nodes;

namespace Crosslight.API.Lang
{
    public abstract class OutputLanguage
    {
        // TODO: replace object with a meaningful type
        public abstract object Encode(Node rootNode);
        public abstract string Name { get; }
        public abstract LanguageConfig Config { get; protected set; }
        public abstract ILanguageOptions Options { get; set; }
        /// <summary>
        /// Set language config and options based on the parameter.
        /// </summary>
        /// <param name="config">Language config to load options from.</param>
        public abstract void LoadOptionsFromConfig(LanguageConfig config);
    }
}
