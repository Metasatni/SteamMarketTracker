using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace SteamMarketTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string appName = "SteamMarketTracker";
         private ServiceProvider _serviceProvider;
        public App()
        {
            var ServiceCollection = new ServiceCollection();
            ServiceCollection.AddSingleton<Database>();
            _serviceProvider = ServiceCollection.BuildServiceProvider();
            ServiceContainer.Initailize(_serviceProvider);
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            CreateDataFolderIfNoExists();
            var MainWindow = new MainWindow();
            MainWindow.Show();
        }
        private void CreateDataFolderIfNoExists()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (Directory.GetDirectories(folder).Contains(Path.Combine(folder, appName)))
            {
                return;
            }
            else
            {
                string specificFolder = Path.Combine(folder, appName);
                Directory.CreateDirectory(specificFolder);
            }

        }
    }
}
