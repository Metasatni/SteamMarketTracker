using Newtonsoft.Json;
using SteamMarketTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
                var allItemsCount = responseModel.total_count;
                var fullName = GetBetween(responseModels[i], "data-hash-name=\"", "\"");
                var imageUrl = GetBetween(responseModels[i], "\"result_" + (i - 1).ToString() + "_image\" src=\"", "\"");
                var url = GetBetween(responseModels[i], "href=\"", "\"");
                var currency = Convert.ToInt32(GetBetween(responseModels[i], "data-currency=\"", "\""));
                var price = GetBetween(responseModels[i], "data-currency=\"" + currency + "\">", "</span>");
                items.Add(new FoundItem(allItemsCount, fullName, imageUrl, url, currency, price));
            }
            return items;
        }
        public async Task<SearchResponseModel> GetResponseModel(string name, string sortType, int count, string appid)
        {
            if (sortType == null)
            {
                string url = "https://steamcommunity.com/market/search/render/?query=" + name + "&count="+count + "&appid=" + appid;
                return await TryGetResponseModel(url);
            }
            else
            {
                string url = "https://steamcommunity.com/market/search/render/?query=" + name + sortType + "&count="+count + "&appid=" + appid;
                return await TryGetResponseModel(url);
            }
        }
        public async Task<SearchResponseModel> TryGetResponseModel(string url)
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
        public static string GetBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            return "";
        }
    }
}
