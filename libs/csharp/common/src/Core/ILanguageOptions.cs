namespace Crosslight.Core;
using static ILanguage;

/// <summary>
/// Specifies the processing behavior for unsupported nodes.
/// </summary>
public enum UnsupportedBehaviorType
{
    /// <summary>
    /// Generate a runtime-dependent error and terminate the parsing process.
    /// </summary>
    Throw = 0,
    /// <summary>
    /// Pass unsupported nodes, but parse everything else, including their children. May break the syntax.
    /// </summary>
    Pass = 1,
    /// <summary>
    /// Pass unsupported nodes and children. Should keep the syntax correct unless it's an important node.
    /// </summary>
    Skip = 2,
}

/// <summary>
/// Base type for language-specific parsing options.
/// </summary>
public interface ILanguageOptions
{
    /// <summary>
    /// Whether unsupported nodes and (optionally) their children should be parsed without a payload.
    /// On error terminates, potentially causing a memory leak.
    /// </summary>
    public UnsupportedBehaviorType UnsupportedBehavior { get; init; }

    /// <summary>
    /// A delegate to allocate a certain amount of memory.
    /// </summary>
    public AcquireDelegate? Acquire { get; init; }

    /// <summary>
    /// A delegate to free an allocated chunk of memory.
    /// </summary>
    public ReleaseDelegate? Release { get; init; }
}
