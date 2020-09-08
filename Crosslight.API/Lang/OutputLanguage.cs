using Crosslight.API.Nodes;

namespace Crosslight.API.Lang
{
    public abstract class OutputLanguage
    {
        // TODO: replace object with a meaningful type
        public abstract object Encode(Node rootNode);
        public abstract string Name { get; }
        public abstract LanguageOptions Options { get; protected set; }
    }
}
