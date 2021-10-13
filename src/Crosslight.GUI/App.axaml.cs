using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Crosslight.GUI.ViewModels;
using Crosslight.GUI.ViewModels.Viewports;
using Crosslight.GUI.Views;
using Dock.Model;
using ReactiveUI;
using Splat;
using System.Reflection;

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
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

            // var factory = new ProjectViewportFactory(new ProjectViewportVM());
            // var layout = factory.CreateLayout();
            // factory.InitLayout(layout);

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = new MainWindow()
                {
                    ViewModel = new MainWindowVM()
                    {
                        // MainViewport = new MainViewportVM()
                        // {
                        //     Factory = factory,
                        //     Layout = layout
                        // }
                    }
                };

                // mainWindow.Closing += (sender, e) =>
                // {
                //     if (layout is IDock dock)
                //     {
                //         dock.Close();
                //     }
                // };

                desktop.MainWindow = mainWindow;

                // desktop.Exit += (sennder, e) =>
                // {
                //     if (layout is IDock dock)
                //     {
                //         dock.Close();
                //     }
                // };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
