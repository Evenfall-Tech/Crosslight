using static Crosslight.Core.ILanguage;

namespace Crosslight.Lang.CsharpRef
{
    public class LanguageOptions
    {
        /// <summary>
        /// Whether unsupported nodes and (optionally) their children should be parsed without a payload.
        /// </summary>
        public bool ParseUnsupported { get; init; }

        /// <summary>
        /// A delegate to allocate a certain amount of memory.
        /// </summary>
        public AcquireDelegate Acquire { get; init; }

        /// <summary>
        /// A delegate to free an allocated chunk of memory.
        /// </summary>
        public ReleaseDelegate Release { get; init; }
    }
}
