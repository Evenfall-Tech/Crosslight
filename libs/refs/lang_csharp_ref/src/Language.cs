using Crosslight.Core;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using static Crosslight.Core.ILanguage;

namespace Crosslight.Lang.CsharpRef;

public class Language : ILanguage
{
    private delegate nint NativeAcquireDelegate(nuint size);
    private NativeAcquireDelegate? _nativeAcquire;

    public LanguageOptions Options { get; }

    public GCHandle Handle { get; private set; }

    public Language(Config config)
    {
        Handle = GCHandle.Alloc(this, GCHandleType.Pinned);
        bool parseUnsupported;
        AcquireDelegate acquire;
        ReleaseDelegate release;

        ConfigureMemoryDelegates(config, out acquire, out release);
        parseUnsupported = config.GetString("Parsing/ProcessUnsupported") == "true";

        Options = new LanguageOptions()
        {
            Acquire = acquire,
            Release = release,
            ParseUnsupported = parseUnsupported,
        };
    }

    private void ConfigureMemoryDelegates(Config config, out AcquireDelegate acquire, out ReleaseDelegate release)
    {
        var memoryAcquire = config.GetString("Memory/Acquire");
        var memoryRelease = config.GetString("Memory/Release");
        AcquireDelegate? acquireLocal = null;
        ReleaseDelegate? releaseLocal = null;

        if (memoryAcquire != null)
        {
            memoryAcquire = memoryAcquire.Replace("0x", "");

            if (nint.TryParse(
                memoryAcquire,
                NumberStyles.HexNumber,
                CultureInfo.InvariantCulture,
                out nint result))
            {
                _nativeAcquire = Marshal.GetDelegateForFunctionPointer<NativeAcquireDelegate>(result);
                acquireLocal = (int size) => _nativeAcquire((nuint)size);
            }
        }

        if (memoryRelease != null)
        {
            memoryRelease = memoryRelease.Replace("0x", "");

            if (nint.TryParse(
                memoryRelease,
                NumberStyles.HexNumber,
                CultureInfo.InvariantCulture,
                out nint result))
            {
                releaseLocal = Marshal.GetDelegateForFunctionPointer<ReleaseDelegate>(result);
            }
        }

        acquireLocal ??= (int size) =>
        {
            try
            {
                return Marshal.AllocCoTaskMem(size);
            }
            catch (OutOfMemoryException)
            {
                return 0;
            }
        };
        releaseLocal ??= Marshal.FreeCoTaskMem;

        acquire = acquireLocal;
        release = releaseLocal;
    }

    /// <inheritdoc/>
    public Node? TransformInput(Resource resource)
    {
        if (ResourceTypesInput?.ContentTypes?.Any(x => x == resource.ContentType) == true)
        {
            return null;
        }

        return null;
    }

    /// <inheritdoc/>
    public Node? TransformModify(Node node)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Resource? TransformOutput(Node node)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public ResourceTypes? ResourceTypesInput => new(new string[] { "text/plain", "text/x-csharp" });

    /// <inheritdoc/>
    public ResourceTypes? ResourceTypesOutput => new(Array.Empty<string>());
}
