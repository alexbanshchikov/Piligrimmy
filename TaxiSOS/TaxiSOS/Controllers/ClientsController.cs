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

        [HttpGet]
        public IEnumerable<Clients> Get()
        {
            return _repo.Get();
        }

        [HttpGet("{id}", Name = "Get")]
        public Clients Get(Guid id)
        {
            return _repo.FindById(id);
        }
        
        [HttpPost]
        public void Create([FromBody]Clients value)
        {
            _repo.Create(value);
        }
        
        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]Clients client)
        {
            if (client.IdClient == id)
                _repo.Update(client);
        }
        
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            Clients c = _repo.FindById(id);
            _repo.Remove(c);
        }
    }
}
