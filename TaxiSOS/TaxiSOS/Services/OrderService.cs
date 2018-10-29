using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Globalization;

namespace TaxiSOS.Services
{
    public class OrderService
    {
        public int Calculate(string From, string To)
        {
            string responseFrom = new WebClient().DownloadString("http://search.maps.sputnik.ru/search?q=" + From);
            var rootFrom = JObject.Parse(responseFrom);
            var latFrom =
                    rootFrom.DescendantsAndSelf()
                        .OfType<JProperty>()
                        .Where(p => p.Name == "lat")
                        .Select(p => p.Value).First();

            var lonFrom =
                    rootFrom.DescendantsAndSelf()
                        .OfType<JProperty>()
                        .Where(p => p.Name == "lon")
                        .Select(p => p.Value).First();

            string responseTo = new WebClient().DownloadString("http://search.maps.sputnik.ru/search?q=" + To);
            var rootTo = JObject.Parse(responseTo);
            var latTo =
                    rootTo.DescendantsAndSelf()
                        .OfType<JProperty>()
                        .Where(p => p.Name == "lat")
                        .Select(p => p.Value).First();

            var lonTo =
                    rootTo.DescendantsAndSelf()
                        .OfType<JProperty>()
                        .Where(p => p.Name == "lon")
                        .Select(p => p.Value).First();

            string route = new WebClient().DownloadString("http://routes.maps.sputnik.ru/osrm/router/viaroute?loc="
                +latFrom.Value<float>().ToString(CultureInfo.GetCultureInfo("en-US")) +","
                +lonFrom.Value<float>().ToString(CultureInfo.GetCultureInfo("en-US")) +"&loc="
                +latTo.Value<float>().ToString(CultureInfo.GetCultureInfo("en-US")) +","
                +lonTo.Value<float>().ToString(CultureInfo.GetCultureInfo("en-US")));
            
            var root = JObject.Parse(route);
            var distance =
                    root.DescendantsAndSelf()
                        .OfType<JProperty>()
                        .Where(p => p.Name == "total_distance")
                        .Select(p => p.Value).First();
            int cost = (int)(distance.Value<int>() * 0.01) + 40;
            return cost;
        }
    }
}
