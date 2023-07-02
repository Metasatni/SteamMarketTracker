using SteamMarketTracker.Models;
using System;
using System.Collections.ObjectModel;

namespace SteamMarketTracker
{
    internal class Database
    {
        private ObservableCollection<SavedItem> _savedItems;
        public ObservableCollection<SavedItem> SavedItems { get { return _savedItems; } set { _savedItems = value; DataChanged?.Invoke("SavedItems"); } }
        public event Action<string> DataChanged;
    }
}
