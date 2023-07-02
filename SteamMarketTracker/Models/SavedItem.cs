using System.Collections.Generic;
using System.Windows.Media;

namespace SteamMarketTracker.Models
{
    public class SavedItem
    {
        public string Name { get; set; }
        public ImageSource ImageSource { get; set; }
        public string Url { get; set; }
        public int Currency { get; set; }
        public List<Price> Prices { get; set; }
        public Price Price { get; set; }
        public SavedItem(string name, ImageSource imageSource, string url, int currency, Price price)
        {
            this.Price = price;
            this.Name = name;
            this.Url = url;
            this.Currency = currency;
            this.ImageSource = imageSource;
        }
    }
}
