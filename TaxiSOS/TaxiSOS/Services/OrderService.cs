using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Globalization;
using DataModel;
using Unity;
using System;

namespace TaxiSOS.Services
{
    public class OrderService
    {     
        IRepository<Drivers> _repo;

        public int Calculate(string From, string To)
        {
            string _latFrom, _lonFrom, _latTo, _lonTo;
            Points(From, out _latFrom, out _lonFrom);            Points(To, out _latTo, out _lonTo);

            string route = new WebClient().DownloadString("http://routes.maps.sputnik.ru/osrm/router/viaroute?loc="
                + _latFrom + "," + _lonFrom + "&loc=" + _latTo + "," + _lonTo);

            var root = JObject.Parse(route);            var distance =
                    root.DescendantsAndSelf()
                        .OfType<JProperty>()
                        .Where(p => p.Name == "total_distance")
                        .Select(p => p.Value).First().Value<int>();            int cost = (int)(distance * 0.01) + 40;
            return cost;
        }

        public void Points(string point, out string lat, out string lon)
        {
            string response = new WebClient().DownloadString("http://search.maps.sputnik.ru/search?q=" + point);
            var root = JObject.Parse(response);
            lat = root.DescendantsAndSelf()
                    .OfType<JProperty>()
                    .Where(p => p.Name == "lat")
                    .Select(p => p.Value).First()
                    .Value<float>()
                    .ToString(CultureInfo.GetCultureInfo("en-US"));

            lon = root.DescendantsAndSelf()
                    .OfType<JProperty>()
                    .Where(p => p.Name == "lon")
                    .Select(p => p.Value).First()
                    .Value<float>()
                    .ToString(CultureInfo.GetCultureInfo("en-US"));
        }

        public Guid FingDriver(IRepository<Drivers> repoDriver)
        {
            _repo = repoDriver;
             var driver = _repo.Get().Where(dr => dr.Status==0).First();
            driver.Status = 1;
            return driver.IdDriver;
        }
    }
}
            