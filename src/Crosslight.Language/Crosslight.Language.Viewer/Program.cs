using Avalonia;
using Avalonia.ReactiveUI;
using Crosslight.Language.Viewer.Avalonia;

namespace Crosslight.Language.Viewer
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args) => BuildAvaloniaApp(ConfigureDefault())
            .StartWithClassicDesktopLifetime(args);

        public static int LaunchApplication(ApplicationOptions options)
        {
            AppBuilder builder;
            if (options != null)
            {
                builder = ConfigureLang(options);
            }
            else
            {
                builder = ConfigureDefault();
            }
            return BuildAvaloniaApp(builder).StartWithClassicDesktopLifetime(new string[] { });
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp(AppBuilder builder)
            => builder
                .UseReactiveUI()
                .UsePlatformDetect()
                .LogToTrace();

        public static AppBuilder ConfigureDefault()
            => AppBuilder.Configure<App>();

        public static AppBuilder ConfigureLang(ApplicationOptions options)
            => ConfigureDefault()
                .With(new ApplicationOptions()
                {
                    Options = options.Options,
                    RootNode = options.RootNode,
                });

    }
}
