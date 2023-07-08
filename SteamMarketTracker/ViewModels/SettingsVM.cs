using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketTracker.ViewModels
{
    public class SettingsVM : ViewModel
    {
        private bool _settingsUcShow;
        public bool SettingsUcShow { get { return _settingsUcShow; } set { _settingsUcShow = value; OnPropertyChanged(); } }
    }
}
