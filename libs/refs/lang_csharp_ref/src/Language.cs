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

    public GCHandle Handle { get; private set; }

    public AcquireDelegate Acquire { get; private set; }

    public ReleaseDelegate Release { get; private set; }

    public Language(Config config)
    {
        Handle = GCHandle.Alloc(this, GCHandleType.Pinned);
        ConfigureMemoryDelegates(config);

        Acquire ??= Marshal.AllocCoTaskMem;
        Release ??= Marshal.FreeCoTaskMem;
    }

    private void ConfigureMemoryDelegates(Config config)
    {
        var memoryAcquire = config.GetString("Memory/Acquire");
        var memoryRelease = config.GetString("Memory/Release");

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
                Acquire = (int size) => _nativeAcquire((nuint)size);
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
                Release = Marshal.GetDelegateForFunctionPointer<ReleaseDelegate>(result);
            }
        }
    }

    /// <inheritdoc/>
    public Node? TransformInput(Resource resource)
    {
        Console.WriteLine(resource.ContentType);
        Console.WriteLine(Encoding.UTF8.GetString(resource.Content!));

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
