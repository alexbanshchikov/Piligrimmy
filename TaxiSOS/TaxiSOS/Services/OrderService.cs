using DataModel;
using DataModel.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;

namespace TaxiSOS.Services
{
    public class OrderService
    {     
        IRepository<Drivers> _repo;    
        public int Calculate(string From, string To)
        {
            string _latFrom, _lonFrom, _latTo, _lonTo;
            Points(From, out _latFrom, out _lonFrom);            Points(To, out _latTo, out _lonTo);

            double x = Convert.ToDouble(Convert.ToDouble(_latFrom) - Convert.ToDouble(_latTo));
            double y = Convert.ToDouble(Convert.ToDouble(_lonFrom) - Convert.ToDouble(_lonTo));

            var distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));            int cost = (int)(distance * 1500) + 40;
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
                    .ToString();

            lon = root.DescendantsAndSelf()
                    .OfType<JProperty>()
                    .Where(p => p.Name == "lon")
                    .Select(p => p.Value).First()
                    .Value<float>()
                    .ToString();
        }

        public Guid FindDriver(IRepository<Drivers> repoDriver)
        {
            _repo = repoDriver;
            var driver = _repo.Get().Where(dr => dr.Status==0).First();
            driver.Status = 1;
            repoDriver.Update(driver);
            return driver.IdDriver;
        }
    }
}
            