using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.Drawing;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace TaxiSOS.Services
{
    public class OrderService
    {
        public int Calculate(string From, string To)
        {

            string responsetext = new WebClient().DownloadString("http://search.maps.sputnik.ru/search?q=" + From);
            var rootFrom = JObject.Parse(responsetext);
            var latFrom =
                    rootFrom.DescendantsAndSelf()
                        .OfType<JProperty>()
                        .Where(p => p.Name == "lat")
                        .Select(p => p.Value).First();
            var l = latFrom.Values(0);
            var lonFrom =
                    rootFrom.DescendantsAndSelf()
                        .OfType<JProperty>()
                        .Where(p => p.Name == "lon")
                        .Select(p => p.Value).First();

            string responsetextTo = new WebClient().DownloadString("http://search.maps.sputnik.ru/search?q=" + To);
            var rootTo = JObject.Parse(responsetext);
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
            string route = new WebClient().DownloadString("http://routes.maps.sputnik.ru/osrm/router/viaroute?loc="+ latFrom.ToString()+ ","+ lonFrom.ToString() + "&"+"loc="+ latTo.ToString() + ","+ lonTo.ToString() + "&instructions=true");

            var root = JObject.Parse(route);
            var distance =
                    root.DescendantsAndSelf()
                        .OfType<JProperty>()
                        .Where(p => p.Name == "total_distance")
                        .Select(p => p.Value);

            // route.Stroke = new Pen(Color.Red, 3); //Задаем цвет и ширину линии
            return 0;
        }
    }
}
