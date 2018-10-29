﻿using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Globalization;
using DataModel;
using Unity;

namespace TaxiSOS.Services
{
    public class OrderService
    {     
        IRepository<Drivers> _repo;

        public int Calculate(string From, string To)
        {
            string _latFrom, _lonFrom, _latTo, _lonTo;
            Points(From, out _latFrom, out _lonFrom);

            string route = new WebClient().DownloadString("http://routes.maps.sputnik.ru/osrm/router/viaroute?loc="
                + _latFrom + "," + _lonFrom + "&loc=" + _latTo + "," + _lonTo);

            var root = JObject.Parse(route);
                    root.DescendantsAndSelf()
                        .OfType<JProperty>()
                        .Where(p => p.Name == "total_distance")
                        .Select(p => p.Value).First().Value<int>();
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

        public Drivers FingDriver(IRepository<Drivers> repoDriver)
        {
            _repo = repoDriver;
             var driver = _repo.Get().Where(dr => dr.Status==0).First();
            return driver;
        }
    }
}
            