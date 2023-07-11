using Newtonsoft.Json;
using SteamMarketTracker.Managers;
using SteamMarketTracker.Models;
using SteamMarketTracker.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SteamMarketTracker.ViewModels
{
    public class TrackedItemsVM : ViewModel
    {
        private CancellationToken token;
        private bool _refreshingState;
        private bool _trackedItemsUcShow;
        private int _refreshTime;
        private Database _database => ServiceContainer.GetService<Database>();
        private ObservableCollection<SavedItem> _savedItems;
        public ObservableCollection<SavedItem> SavedItems { get { return _savedItems; } set { _savedItems = value; OnPropertyChanged(); } }
        public bool RefreshingState { get { return _refreshingState; } set { _refreshingState = value; OnPropertyChanged(); } }

        public int RefreshTime { get { return _refreshTime; } set { _refreshTime = value; OnPropertyChanged(); } }
        public bool TrackedItemsUcShow { get { return _trackedItemsUcShow; } set { _trackedItemsUcShow = value; OnPropertyChanged(); } }
        public ICommand RefreshCommand { get; }
        public ICommand FavoriteClickCommand { get; }
        public ICommand HistoryClickCommand { get; }

        public TrackedItemsVM()
        {
            RefreshTime = 60;
            _savedItems = _database.SavedItems;
            _database.DataChanged += _database_DataChanged;
            this.RefreshCommand = new Command(
              execute: Refresh);
            this.FavoriteClickCommand = new Command(
                execute: FavoriteClick);
            this.HistoryClickCommand = new Command(
                execute: HistoryClick);
        }
        private void FavoriteClick(object? obj)
        {
            string str = (string)obj;
            var item = _database.SavedItems.Where(x => x.Url == str).FirstOrDefault();
            FileManager.SaveItemToDataFile(item);
            Refresh();
        }
        private void HistoryClick(object? obj)
        {
            string str = (string)obj;
            var item = _database.SavedItems.Where(x => x.Url == str).FirstOrDefault();
            if (item != null)
            {
                ItemHistoryWindow itemHistoryWindow = new ItemHistoryWindow();
                itemHistoryWindow.DataContext = new ItemHistoryWindowVM(item);
                itemHistoryWindow.Show();
            }
        }
        private void Refresh(object? obj)
        {
            var dataFile = FileManager.GetDataFile();
            if (dataFile != "")
            {
                _database.SavedItems = JsonConvert.DeserializeObject<ObservableCollection<SavedItem>>(dataFile);
            }
        }
        private void Refresh()
        {
            var dataFile = FileManager.GetDataFile();
            if (dataFile != "")
            {
                _database.SavedItems = JsonConvert.DeserializeObject<ObservableCollection<SavedItem>>(dataFile);
            }
        }
        private async void RefreshItemsPrice()
        {
            var list = _database.SavedItems;
            foreach (var item in _database.SavedItems)
            {
                if (list != _database.SavedItems)
                {
                    RefreshingState = false;
                    return;
                }
                await Task.Delay(20000).WaitAsync(cancellationToken: token);
                item.RefreshPrice();

            }
            RefreshingState = false;
            RefreshItemsPrice();
        }

        private void _database_DataChanged(string obj)
        {
            SavedItems = _database.SavedItems;
            if (_database.SavedItems.Count > 0)
            {
                RefreshingState = true;
                RefreshItemsPrice();
            }
        }
    }
}
