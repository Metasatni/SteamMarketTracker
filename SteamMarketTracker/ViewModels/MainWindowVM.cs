using Newtonsoft.Json;
using SteamMarketTracker.Managers;
using SteamMarketTracker.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SteamMarketTracker.ViewModels
{
    public class MainWindowVM : ViewModel
    {
        private Database _database => ServiceContainer.GetService<Database>();
        private SearchItemVM _searchItemVM;
        private TrackedItemsVM _trackedItemsVM;
        public SearchItemVM SearchItemVM { get { return _searchItemVM; } set { _searchItemVM = value; OnPropertyChanged(); } }
        public TrackedItemsVM TrackedItemsVM { get { return _trackedItemsVM; } set { _trackedItemsVM = value; OnPropertyChanged(); } }

        public ICommand ShowSearchItemUcCommand { get; }
        public ICommand ShowTrackedItemsUcCommand { get; }
        public MainWindowVM()
        {
            this.SearchItemVM = new SearchItemVM();
            this.TrackedItemsVM = new TrackedItemsVM();
            this.ShowSearchItemUcCommand = new Command(
              execute: ShowSearchItemUC);
            this.ShowTrackedItemsUcCommand = new Command(
              execute: ShowTrackedItemsUC);
            _database.SavedItems = GetSavedItemsFromDataFile();
        }
        private void ShowSearchItemUC(object? _)
        {
            ChangeOneVisibility(true, nameof(SearchItemVM.SearchItemUcShow));
        }
        private void ShowTrackedItemsUC(object? _)
        {
            ChangeOneVisibility(true, nameof(TrackedItemsVM.TrackedItemsUcShow));
        }
        private void ChangeOneVisibility(bool visible, string name)
        {
            SearchItemVM.SearchItemUcShow = visible ? name == nameof(SearchItemVM.SearchItemUcShow) : !visible;
            TrackedItemsVM.TrackedItemsUcShow = visible ? name == nameof(TrackedItemsVM.TrackedItemsUcShow) : !visible;
        }
        private ObservableCollection<SavedItem> GetSavedItemsFromDataFile()
        {
            var dataFile = SMTFileManager.GetDataFile();
            if(dataFile != "")
            {
                return JsonConvert.DeserializeObject<ObservableCollection<SavedItem>>(dataFile);
            }
            return new ObservableCollection<SavedItem>();
        }
    }
}
