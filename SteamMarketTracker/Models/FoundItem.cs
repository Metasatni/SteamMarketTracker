using System.Windows.Media;

namespace SteamMarketTracker.Models
{
    public class FoundItem : ViewModel
    {
        public string Name { get; set; }
        public ImageSource ImageSource { get; set; }
        public string Url { get; set; }
        public int Currency { get; set; }
        public Price Price { get; set; }
        private bool _favorite;

        public bool Favorite { get => _favorite; set { _favorite = value; OnPropertyChanged(); } }
        public FoundItem(string name, string imageUrl, string url, int currency, Price price)
        {
            var converter = new ImageSourceConverter();
            this.Name = name;
            if (imageUrl != null)
            {
                this.ImageSource = (converter.ConvertFromString(imageUrl) as ImageSource);
            }
            this.Url = url;
            this.Price = price;
            this.Currency = currency;
        }
    }
}
