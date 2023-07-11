using Newtonsoft.Json;
using SteamMarketTracker.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Documents;

namespace SteamMarketTracker.Managers
{
    public class FileManager
    {
        static Database _database => ServiceContainer.GetService<Database>();
        public static void SaveItemToDataFile(object item)
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SteamMarketTracker");
            string filePath = Path.Combine(folder, "FavoriteItems.json");
            if (File.Exists(filePath))
            {
                var file = File.ReadAllText(filePath);
                var serializedItems = JsonConvert.DeserializeObject<ObservableCollection<FoundItem>>(file);
                if (item is SavedItem)
                {
                    var obj = (SavedItem)item;
                    serializedItems.Remove(serializedItems.Where(x => x.Url == obj.Url).FirstOrDefault());
                }
                else
                {
                    var obj = (FoundItem)item;
                    if (serializedItems.Where(x => x.Url == obj.Url).Any())
                    {
                        var itemToRemove = serializedItems.Where(x => x.Url == obj.Url).FirstOrDefault();
                        serializedItems.Remove(itemToRemove);
                    }
                    else
                    {
                        serializedItems.Add(obj);
                    }
                }

                File.WriteAllText(filePath, JsonConvert.SerializeObject(serializedItems, Formatting.Indented));
            }
            else
            {
                var obj = (FoundItem)item;
                ObservableCollection<FoundItem> items = new ObservableCollection<FoundItem> { obj };
                File.WriteAllText(filePath, JsonConvert.SerializeObject(items, Formatting.Indented));
            }
        }
        public static void UpdateDataFile()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SteamMarketTracker");
            string filePath = Path.Combine(folder, "FavoriteItems.json");
            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(_database.SavedItems, Formatting.Indented));
            }
        }
        public static string GetDataFile()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SteamMarketTracker");
            string filePath = Path.Combine(folder, "FavoriteItems.json");
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return "";
        }
    }
}
