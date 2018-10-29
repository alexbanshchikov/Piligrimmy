using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxiSOS.Services;
using DataModel;

namespace TaxiSOS.Controllers
{
    [Produces("application/json")]
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        OrderService os = new OrderService();
        private readonly IRepository<Drivers> _repoDriver = null;
        private readonly IRepository<Drivers> _repoOrder = null;
        public OrdersController(IRepository<Drivers> repoDriver , IRepository<Drivers> repoOrder)
        {
            _repoDriver = repoDriver;
            _repoOrder = repoOrder;

        }
        // GET: api/Orders
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var d = os.FingDriver(_repoDriver);
            return new string[] { "value1", "value2" };
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("{calc}")]
        public int Calculate(string From, string To)
        {
            return os.Calculate(From, To);
        }

        // POST: api/Orders
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
