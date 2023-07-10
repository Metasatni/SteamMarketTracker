using SteamMarketTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketTracker.ViewModels
{
    public class ItemHistoryWindowVM : ViewModel
    {
        private SavedItem _savedItem;
        public SavedItem SavedItem { get { return _savedItem; } set { _savedItem = value; OnPropertyChanged(); } }
        public ItemHistoryWindowVM(SavedItem item)
        {
            SavedItem = item;
        }
    }
}
