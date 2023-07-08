using SteamMarketTracker.Managers;
using SteamMarketTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SteamMarketTracker.Models
{
    public class SavedItem : ViewModel
    {
        public string Name { get; set; }
        public ImageSource ImageSource { get; set; }
        public string Url { get; set; }
        public int Currency { get; set; }
        public ObservableCollection<Price> Prices { get; set; }
        private Price _price;
        public Price Price { get { return _price; } set { _price = value; OnPropertyChanged(); } }
        public string AppId { get; set; }
        public SavedItem()
        {

        }
        public SavedItem(string name, ImageSource imageSource, string url, int currency, Price price, string appId, ObservableCollection<Price> prices)
        {
            this.Prices = prices; 
            this.Price = price;
            this.Name = name;
            this.Url = url;
            this.Currency = currency;
            this.ImageSource = imageSource;
            this.AppId = appId;
        }
        public async void RefreshPrice()
        {
            var searchService = new SearchService();
            var item = await searchService.GetItemAsync(this.Name, this.AppId);
            var item2 = item.FirstOrDefault();
            if (item2 != null)
            {
                if (item2.Price.Value != this.Price.Value)
                {
                    this.Prices.Add(this.Price);
                    this.Price = new Price(DateTime.Now, item2.Price.Value);
                    FileManager.UpdateDataFile();
                }
            }
        }
    }
}
