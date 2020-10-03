using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Crosslight.GUI.ViewModels.Explorers;
using Crosslight.GUI.Views;
using Splat;

namespace Crosslight.GUI
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new LanguagesVM());
            Locator.CurrentMutable.RegisterLazySingleton(() => new PropertiesVM());
            Locator.CurrentMutable.RegisterLazySingleton(() => new SourceInputVM());
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow()
                {
                    ViewModel = new ViewModels.MainWindowVM()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
