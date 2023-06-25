using SteamMarketTracker.ViewModels;
using System.Windows.Controls;

namespace SteamMarketTracker.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy SearchItemUC.xaml
    /// </summary>
    public partial class SearchItemUC : UserControl
    {
        public SearchItemUC()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new SearchItemVM();
        }
    }
}
