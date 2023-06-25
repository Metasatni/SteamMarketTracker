using SteamMarketTracker.ViewModels;
using System.Windows;

namespace SteamMarketTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var MainWindow = new MainWindow();
            MainWindow.Show();
        }
    }
}
