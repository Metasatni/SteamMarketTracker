namespace SteamMarketTracker.Models
{
    internal class SearchResponseModel
    {
        public bool success { get; set; }
        public int start { get; set; }
        public int pagesize { get; set; }
        public int total_count { get; set; }
        public string tip { get; set; }
        public string results_html { get; set; }
    }
}
