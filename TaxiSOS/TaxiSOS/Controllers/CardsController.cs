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
    [Route("api/Cards")]
    public class CardsController : Controller
    {
        private readonly IRepository<Cards> _repo = null;
        public CardsController(IRepository<Cards> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Cards> Get()
        {
            return _repo.Get();
        }

        [HttpGet("{id}")]
        public Cards Get(string number)
        {
            return _repo.Get().Where(dr => dr.CardNumber == number).First();
        }

        [HttpPost]
        public void Create([FromBody]Cards value)
        {
            _repo.Create(value);
        }

        [HttpPut("{id}")]
        public void Update(string number, [FromBody]Cards card)
        {
            if (card.CardNumber == number)
                _repo.Update(card);
        }

        [HttpDelete("{id}")]
        public void Delete(string number)
        {
            var cards = _repo.Get().Where(dr => dr.CardNumber == number).First();
            _repo.Remove(cards);
        }
    }
}
