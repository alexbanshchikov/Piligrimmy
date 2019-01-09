﻿using DataModel;
using DataModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [Authorize]
        [HttpGet]
        public IEnumerable<Cards> Get(Guid id)
        {
            return _repo.Get().Where(x => x.IdClient == id);
        }

        [Authorize]
        [HttpGet("{number}")]
        public Cards Get(string number)
        {
            return _repo.Get().Where(dr => dr.CardNumber == number).First();
        }

        [Authorize]
        [HttpPost]
        public void Create([FromBody]Cards value)
        {
            _repo.Create(value);
        }

        [Authorize]
        [HttpPut("{number}")]
        public void Update(string number, [FromBody]Cards card)
        {
            if (card.CardNumber == number)
                _repo.Update(card);
        }

        [Authorize]
        [HttpDelete("{number}")]
        public void Delete(string number)
        {
            var cards = _repo.Get().Where(dr => dr.CardNumber == number).First();
            _repo.Remove(cards);
        }
    }
}
