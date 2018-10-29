﻿using System;
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
        private readonly IRepository<Orders> _repoOrder = null;
        public OrdersController(IRepository<Drivers> repoDriver, IRepository<Orders> repoOrder)
        {
            _repoDriver = repoDriver;
            _repoOrder = repoOrder;

        }
        [HttpGet]
        public IEnumerable<Orders> Get()
        {
            return _repoOrder.Get();
        }

        [HttpGet("{id}")]
        public Orders Get(Guid id)
        {
            return _repoOrder.FindById(id);
        }

        [HttpPost]
        public void Create([FromBody]Orders value)
        {
            var driver = os.FingDriver(_repoDriver);
            _repoOrder.Create(value);
        }

        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]Orders driver)
        {
            if (driver.IdDriver == id)
                _repoOrder.Update(driver);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            Orders c = _repoOrder.FindById(id);
            Drivers driver = _repoDriver.FindById(c.IdDriver);
            driver.Status = 0;
            _repoOrder.Remove(c);
        }
        [HttpGet("{calc}")]
        public int Calculate(string From, string To)
        {
            return os.Calculate(From, To);
        }

    }
}
