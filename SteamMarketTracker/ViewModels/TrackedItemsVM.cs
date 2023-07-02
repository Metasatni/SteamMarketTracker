using Newtonsoft.Json;
using SteamMarketTracker.Managers;
using SteamMarketTracker.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SteamMarketTracker.ViewModels
{
    public class TrackedItemsVM : ViewModel
    {
        private Database _database => ServiceContainer.GetService<Database>();
        private ObservableCollection<SavedItem> _savedItems;
        public ObservableCollection<SavedItem> SavedItems { get { return _savedItems; } set { _savedItems = value; OnPropertyChanged(); } }
        private bool _trackedItemsUcShow;
        public bool TrackedItemsUcShow { get { return _trackedItemsUcShow;} set { _trackedItemsUcShow = value; OnPropertyChanged(); } }
        public ICommand RefreshCommand { get; }
        public ICommand FavoriteClickCommand { get; }
        
        public TrackedItemsVM()
        {
            _savedItems = _database.SavedItems;
            _database.DataChanged += _database_DataChanged;
            this.RefreshCommand = new Command(
              execute: Refresh);
            this.FavoriteClickCommand = new Command(
                execute: FavoriteClick);
        }
        private void FavoriteClick(object? obj)
        {
            string str = (string)obj;
            var item = _database.SavedItems.Where(x => x.Url == str).FirstOrDefault();
            SMTFileManager.SaveItemToDataFile(item);
            Refresh();
        }
        private void Refresh(object? obj)
        {
            var dataFile = SMTFileManager.GetDataFile();
            if(dataFile != "")
            {
                _database.SavedItems = JsonConvert.DeserializeObject<ObservableCollection<SavedItem>>(dataFile);
            }
        }
        private void Refresh()
        {
            var dataFile = SMTFileManager.GetDataFile();
            if(dataFile != "")
            {
                _database.SavedItems = JsonConvert.DeserializeObject<ObservableCollection<SavedItem>>(dataFile);
            }
        }

        private void _database_DataChanged(string obj)
        {
            SavedItems = _database.SavedItems;
        }
    }
}
