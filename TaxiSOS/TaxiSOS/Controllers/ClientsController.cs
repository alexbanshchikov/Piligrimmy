using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel;
using DataModel.Models;
using Microsoft.AspNetCore.Mvc;
using TaxiSOS.Services;

namespace TaxiSOS.Controllers
{
    [Produces("application/json")]
    [Route("api/Clients")]
    public class ClientsController : Controller
    {
        private readonly IRepository<Clients> _repo = null;
        private readonly IRepository<Drivers> _repoDriver = null;
        private readonly IRepository<Cars> _repoCars = null;

        EmailService em = new EmailService();

        public ClientsController(IRepository<Clients> repo, IRepository<Drivers> repoDriver, IRepository<Cars> repoCars)
        {
            _repo = repo;
            _repoDriver = repoDriver;
            _repoCars = repoCars;
        }


        [HttpGet]
        public IEnumerable<Clients> Get()
        {
            return _repo.Get();
        }

        [HttpGet("{id}")]
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


        [HttpGet("SendMessageFinish")]
        public void SendMessage(Guid id)
        {
            var client = _repo.FindById(id);
            em.SendEmailAsync(client.Email, "TaxiSOS", "Водитель на месте");
        }

        [HttpGet("SendMessageAssigned")]
        public void SendMessageAssigned(Guid id, Guid idDriver)
        {
            var client = _repo.FindById(id);
            var cars = _repoCars.Get().Where(ca => ca.IdDriver == idDriver).First();
            em.SendEmailAsync(client.Email, "TaxiSOS", $"Водитель назначен. К вам прибудет {cars.Color} {cars.Mark} с номером {cars.RegistrationNumber}");
        }

    }
}
