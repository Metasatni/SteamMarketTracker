using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketTracker.ViewModels
{
    public class SavedItemsVM : ViewModel
    {
        private bool _savedItemsUcShow;
        public bool SavedItemsUcShow { get { return _savedItemsUcShow; } set { _savedItemsUcShow = value; OnPropertyChanged(); } }
    }
}
