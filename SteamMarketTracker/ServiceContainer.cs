using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMarketTracker
{
    static class ServiceContainer
    {
        private static IServiceProvider _serviceProvider;
        public static void Initailize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }
        public static T GetService<T>()
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }
    }
}
