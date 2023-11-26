using Crosslight.Core;
using System.Globalization;
using System.Runtime.InteropServices;
using static Crosslight.Core.ILanguage;

namespace Crosslight.Lang.CsharpRef
{
    internal class LanguageOptions : ILanguageOptions
    {
        private delegate nint NativeAcquireDelegate(nuint size);

        public LanguageOptions(Config config)
        {
            AcquireDelegate acquire;
            ReleaseDelegate release;

            ConfigureMemoryDelegates(config, out acquire, out release);

            string? unsupportedBehavior = config.GetString("Parsing/UnsupportedBehavior");
            unsupportedBehavior = unsupportedBehavior?.ToLower();

            UnsupportedBehavior = unsupportedBehavior switch
            {
                "0" => UnsupportedBehaviorType.Throw,
                "throw" => UnsupportedBehaviorType.Throw,
                "1" => UnsupportedBehaviorType.Pass,
                "pass" => UnsupportedBehaviorType.Pass,
                "2" => UnsupportedBehaviorType.Skip,
                "skip" => UnsupportedBehaviorType.Skip,
                _ => UnsupportedBehaviorType.Throw,
            };
            Acquire = acquire;
            Release = release;
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
                    var nativeAcquire = Marshal.GetDelegateForFunctionPointer<NativeAcquireDelegate>(result);
                    acquireLocal = (int size) => nativeAcquire((nuint)size);
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

        public UnsupportedBehaviorType UnsupportedBehavior { get; init; }
        public AcquireDelegate? Acquire { get; init; }
        public ReleaseDelegate? Release { get; init; }
    }
}
