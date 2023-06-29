using SteamMarketTracker.Models;
using SteamMarketTracker.Services;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace SteamMarketTracker.ViewModels
{
    public class SearchItemVM : ViewModel
    {
        private bool _searchItemUcShow;
        private int _sliderValue;
        private string _appid;
        private string _sortType;
        private string _searchString;
        private ObservableCollection<FoundItem> _foundItems;
        public ObservableCollection<FoundItem> FoundItems { get { return _foundItems; } set { _foundItems = value; OnPropertyChanged(); } }
        public int SliderValue { get { return _sliderValue; } set { _sliderValue = value; OnPropertyChanged(); } }
        public string AppId { get { return _appid; } set { _appid = value; OnPropertyChanged(); } }
        public bool SearchItemUcShow { get { return _searchItemUcShow; } set { _searchItemUcShow = value; OnPropertyChanged(); } }
        public string SearchString { get { return _searchString; } set { _searchString = value; OnPropertyChanged(); } }
        public string SortType { get { return _sortType; } set { _sortType = value; OnPropertyChanged(); } }

        public ICommand SearchCommand { get; }
        public ICommand SortTypeCommand { get; }
        public SearchItemVM()
        {
            this.AppId = string.Empty;
            this.SliderValue = 10;
            this.SearchCommand = new Command(
              execute: Search);
            SortTypeCommand = new Command(
                execute: Sort);
        }
        private void Sort(object obj)
        {
            var args = (SelectionChangedEventArgs)obj;
            var item = (ComboBoxItem)args.AddedItems[0];
            var name = item.Tag.ToString();
            SortType = name;
        }
        private async void Search(object? _)
        {
            var service = new SearchService();
            var items = await service.GetItemsAsync(SearchString, SortType, SliderValue, AppId);
            if (items != null)
                FoundItems = new ObservableCollection<FoundItem>(items);
            else
                return;
        }
    }
}
