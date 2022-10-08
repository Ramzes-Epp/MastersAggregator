using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MasterAggregator.Desktop.ViewModels;
using MasterAggregator.Desktop.Views;
using MasterAggregator.Desktop.Repositories.TestData;
using MasterAggregator.Desktop.Repositories.WebApiJson;
using MasterAggregator.Desktop.Repositories;
using System.Net.Http;

namespace MasterAggregator.Desktop
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    // DataContext = new MainWindowViewModel(new UserRepositoryDesktop(), new MasterRepositoryDesktop())//подключаем тестовые данные без подключения к БД 
                    DataContext = new MainWindowViewModel(new UserRepositoryDesktopWebApi(), new MasterRepositoryDesktopWebApi()),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}