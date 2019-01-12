using DataModel;
using DataModel.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TaxiSOS.Controllers
{
    [Produces("application/json")]
    [Route("api/Drivers")]
    public class DriversController : Controller
    {
        private readonly IRepository<Drivers> _repo = null;
        public DriversController(IRepository<Drivers> repo)
        {
            _repo = repo;
        }
        // GET: api/Drivers
        [HttpGet]
        public IEnumerable<Drivers> Get()
        {
            return _repo.Get();
        }

        [HttpGet("{id}")]
        public Drivers Get(Guid id)
        {
            return _repo.FindById(id);
        }

        [HttpPost]
        public void Create([FromBody]Drivers value)
        {
            _repo.Create(value);
        }

        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]Drivers driver)
        {
            if (driver.IdDriver == id)
                _repo.Update(driver);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            Drivers c = _repo.FindById(id);
            _repo.Remove(c);
        }
    }
}
