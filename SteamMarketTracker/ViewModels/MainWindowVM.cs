using System.Windows.Input;

namespace SteamMarketTracker.ViewModels
{
    public class MainWindowVM : ViewModel
    {
        private bool _searchItemUcShow;
        private bool _trackedItemsUcShow;
        private bool _savedItemsUcShow;
        public bool SearchItemUCShow { get { return _searchItemUcShow; } set { _searchItemUcShow = value; OnPropertyChanged(); } }
        public bool TrackedItemsUCShow { get { return _trackedItemsUcShow; } set { _trackedItemsUcShow = value; OnPropertyChanged(); } }
        public bool SavedItemsUcShow { get { return _savedItemsUcShow; } set { _savedItemsUcShow = value; OnPropertyChanged(); } }

        public ICommand ShowSearchItemUcCommand { get; }
        public ICommand ShowTrackedItemsUcCommand { get; }
        public ICommand ShowSavedItemsUcCommand { get; }
        public MainWindowVM()
        {
            this.ShowSearchItemUcCommand = new Command(
              execute: ShowSearchItemUC);
            this.ShowTrackedItemsUcCommand = new Command(
              execute: ShowTrackedItemsUC);
            this.ShowSavedItemsUcCommand = new Command(
              execute: ShowSavedItemsUc);
        }
        private void ShowSearchItemUC(object? _)
        {
            ChangeOneVisibility(true,nameof(SearchItemUCShow));
        }
        private void ShowTrackedItemsUC(object? _)
        {
            ChangeOneVisibility(true,nameof(TrackedItemsUCShow));
        }
        private void ShowSavedItemsUc(object? _)
        {
            ChangeOneVisibility(true,nameof(SavedItemsUcShow));
        }
        private void ChangeOneVisibility(bool visible, string name)
        {
            SearchItemUCShow = visible ? name == nameof(SearchItemUCShow) : !visible;
            TrackedItemsUCShow = visible ? name == nameof(TrackedItemsUCShow) : !visible;
            SavedItemsUcShow = visible ? name == nameof(SavedItemsUcShow) : !visible;
        }
    }
}
