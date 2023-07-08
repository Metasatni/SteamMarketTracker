using Newtonsoft.Json;
using SteamMarketTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;

namespace SteamMarketTracker.Services
{
    internal class SearchService
    {
        public async Task<List<FoundItem>> GetItemsAsync(string name, string sortType, int count, string appid)
        {
            var items = new List<FoundItem>();
            var responseModel = await GetResponseModel(name, sortType, count, appid);
            string[] responseModels;
            responseModels = responseModel.results_html.Split("<a class=\"market_listing_row_link\"");
            for (int i = 1; i < responseModels.Count(); i++)
            {
                var fullName = StaticTools.GetBetween(responseModels[i], "data-hash-name=\"", "\"");
                var imageUrl = StaticTools.GetBetween(responseModels[i], "\"result_" + (i - 1).ToString() + "_image\" src=\"", "\"");
                var url = StaticTools.GetBetween(responseModels[i], "href=\"", "\"");
                var currency = Convert.ToInt32(StaticTools.GetBetween(responseModels[i], "data-currency=\"", "\""));
                var price = StaticTools.GetBetween(responseModels[i], "data-currency=\"" + currency + "\">", "</span>");
                var appId = StaticTools.GetBetween(responseModels[i], "data-appid=\"", "\"");
                string digits = string.Join("", price.Where(x => char.IsNumber(x)));
                double priceValue = Convert.ToDouble(digits) / 100;
                items.Add(new FoundItem(fullName, imageUrl, url, currency, new Price(DateTime.Now, priceValue), appId, new ObservableCollection<Price>()));
            }
            return items;
        }
        public async Task<List<FoundItem>> GetItemAsync(string name, string appid)
        {
            var items = new List<FoundItem>();
            string uri = "https://steamcommunity.com/market/search/render/?query=" + name + "&count=" + 1 + "&appid=" + appid;
            var responseModel = await TryGetResponseModel(uri);
            string[] responseModels;
            responseModels = responseModel.results_html.Split("<a class=\"market_listing_row_link\"");
            for (int i = 1; i < responseModels.Count(); i++)
            {
                var fullName = StaticTools.GetBetween(responseModels[i], "data-hash-name=\"", "\"");
                var imageUrl = StaticTools.GetBetween(responseModels[i], "\"result_" + (i - 1).ToString() + "_image\" src=\"", "\"");
                var url = StaticTools.GetBetween(responseModels[i], "href=\"", "\"");
                var currency = Convert.ToInt32(StaticTools.GetBetween(responseModels[i], "data-currency=\"", "\""));
                var price = StaticTools.GetBetween(responseModels[i], "data-currency=\"" + currency + "\">", "</span>");
                var appId = StaticTools.GetBetween(responseModels[i], "data-appid=\"", "\"");
                string digits = string.Join("", price.Where(x => char.IsNumber(x)));
                double priceValue = Convert.ToDouble(digits) / 100;
                items.Add(new FoundItem(fullName, imageUrl, url, currency, new Price(DateTime.Now, priceValue), appId, new ObservableCollection<Price>()));
            }
            return items;
        }
        private async Task<SearchResponseModel> GetResponseModel(string name, string sortType, int count, string appid)
        {
            if (sortType == null)
            {
                string url = "https://steamcommunity.com/market/search/render/?query=" + name + "&count=" + count + "&appid=" + appid;
                return await TryGetResponseModel(url);
            }
            else
            {
                string url = "https://steamcommunity.com/market/search/render/?query=" + name + sortType + "&count=" + count + "&appid=" + appid;
                return await TryGetResponseModel(url);
            }
        }
        private async Task<SearchResponseModel> TryGetResponseModel(string url)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync(url);
                var responseModel = JsonConvert.DeserializeObject<SearchResponseModel>(response);
                return responseModel;
            }
            catch (Exception ex)
            {
                return new SearchResponseModel();
            }
        }
    }
}
