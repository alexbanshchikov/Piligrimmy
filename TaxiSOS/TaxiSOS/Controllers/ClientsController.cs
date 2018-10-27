using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaxiSOS.Controllers
{
    [Produces("application/json")]
    [Route("api/Clients")]
    public class ClientsController : Controller
    {
        private readonly IRepository<Clients> _repo = null;
        public ClientsController(IRepository<Clients> repo)
        {
            _repo = repo;
        }
        // GET: api/Clients
        [HttpGet]
        public IEnumerable<Clients> Get()
        {
            return _repo.Get();
        }

        // GET: api/Clients/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Clients
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Clients/5
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
