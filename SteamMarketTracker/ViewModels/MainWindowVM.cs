using System.Windows.Input;

namespace SteamMarketTracker.ViewModels
{
    public class MainWindowVM : ViewModel
    {
        private SearchItemVM _searchItemVM;
        private TrackedItemsVM _trackedItemsVM;
        private SavedItemsVM _savedItemsVM;
        public SearchItemVM SearchItemVM { get { return _searchItemVM; }set { _searchItemVM = value; OnPropertyChanged(); } }
        public TrackedItemsVM TrackedItemsVM { get { return _trackedItemsVM; }set { _trackedItemsVM = value; OnPropertyChanged(); } }
        public SavedItemsVM SavedItemsVM { get { return _savedItemsVM; }set { _savedItemsVM = value; OnPropertyChanged(); } }

        public ICommand ShowSearchItemUcCommand { get; }
        public ICommand ShowTrackedItemsUcCommand { get; }
        public ICommand ShowSavedItemsUcCommand { get; }
        public MainWindowVM()
        {
            this.SearchItemVM = new SearchItemVM();
            this.TrackedItemsVM = new TrackedItemsVM();
            this.SavedItemsVM = new SavedItemsVM();
            this.ShowSearchItemUcCommand = new Command(
              execute: ShowSearchItemUC);
            this.ShowTrackedItemsUcCommand = new Command(
              execute: ShowTrackedItemsUC);
            this.ShowSavedItemsUcCommand = new Command(
              execute: ShowSavedItemsUc);
        }
        private void ShowSearchItemUC(object? _)
        {
            ChangeOneVisibility(true,nameof(SearchItemVM.SearchItemUcShow));
        }
        private void ShowTrackedItemsUC(object? _)
        {
            ChangeOneVisibility(true,nameof(TrackedItemsVM.TrackedItemsUcShow));
        }
        private void ShowSavedItemsUc(object? _)
        {
            ChangeOneVisibility(true,nameof(SavedItemsVM.SavedItemsUcShow));
        }
        private void ChangeOneVisibility(bool visible, string name)
        {
            SearchItemVM.SearchItemUcShow = visible ? name == nameof(SearchItemVM.SearchItemUcShow) : !visible;
            TrackedItemsVM.TrackedItemsUcShow = visible ? name == nameof(TrackedItemsVM.TrackedItemsUcShow) : !visible;
            SavedItemsVM.SavedItemsUcShow = visible ? name == nameof(SavedItemsVM.SavedItemsUcShow) : !visible;
        }
    }
}
