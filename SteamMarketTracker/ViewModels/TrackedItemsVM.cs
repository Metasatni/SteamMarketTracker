using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketTracker.ViewModels
{
    public class TrackedItemsVM : ViewModel
    {
        private bool _trackedItemsUcShow;
        public bool TrackedItemsUcShow { get { return _trackedItemsUcShow;} set { _trackedItemsUcShow = value; OnPropertyChanged(); } }
    }
}
