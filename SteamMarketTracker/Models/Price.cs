using System;

namespace SteamMarketTracker.Models
{

    public class Price
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
        public Price()
        {
        }
        public Price(DateTime dateTime, double value)
        {
            this.DateTime = dateTime;
            this.Value = value;
        }
    }
}
