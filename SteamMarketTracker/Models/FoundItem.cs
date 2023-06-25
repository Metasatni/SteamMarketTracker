using System.Windows.Media;

namespace SteamMarketTracker.Models
{
    public class FoundItem
    {
        public string Name { get; set; }
        public ImageSource ImageSource { get; set; }
        public string Url { get; set; }
        public int Currency { get; set; }
        public string Price { get; set; }
        public FoundItem(int allItemsCount, string name, string imageUrl, string url, int currency, string price)
        {
            var converter = new ImageSourceConverter();
            this.Name = name;
            this.ImageSource = (converter.ConvertFromString(imageUrl) as ImageSource);
            this.Url = url;
            this.Price = price;
            this.Currency = currency;
        }
    }
}
