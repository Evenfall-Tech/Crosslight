using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging.Serilog;
using Crosslight.Viewer.Avalonia;

namespace Crosslight.Viewer
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => ConfigureDefault()
                .UsePlatformDetect()
                .LogToDebug();

        public static AppBuilder ConfigureDefault()
            => AppBuilder.Configure<App>();

        public static AppBuilder ConfigureLang()
            => ConfigureDefault()
                .With(new ApplicationOptions()
                {
                    Options = null,
                    RootNode = null,
                });

    }
}
