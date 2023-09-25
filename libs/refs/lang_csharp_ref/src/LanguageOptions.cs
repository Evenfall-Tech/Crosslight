using Crosslight.Core;
using System.Globalization;
using System.Runtime.InteropServices;
using static Crosslight.Core.ILanguage;

namespace Crosslight.Lang.CsharpRef
{
    internal class LanguageOptions
    {
        private delegate nint NativeAcquireDelegate(nuint size);

        /// <summary>
        /// Whether unsupported nodes and (optionally) their children should be parsed without a payload.
        /// On error terminates, potentially causing a memory leak.
        /// </summary>
        public bool ParseUnsupported { get; init; }

        /// <summary>
        /// A delegate to allocate a certain amount of memory.
        /// </summary>
        public AcquireDelegate? Acquire { get; init; }

        /// <summary>
        /// A delegate to free an allocated chunk of memory.
        /// </summary>
        public ReleaseDelegate? Release { get; init; }

        public LanguageOptions(Config config)
        {
            AcquireDelegate acquire;
            ReleaseDelegate release;

            ConfigureMemoryDelegates(config, out acquire, out release);

            ParseUnsupported = config.GetString("Parsing/ProcessUnsupported") == "true";
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
    }
}
