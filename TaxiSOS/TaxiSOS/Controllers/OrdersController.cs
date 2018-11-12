﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxiSOS.Services;
using DataModel;
using static DataModel.Enums.StatusEnum;

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
        public void Create([FromBody]Orders order)
        {
            order.OrderTime = DateTime.Now;
            order.Cost = Calculate(order.ArrivalPoint, order.DestinationPoint);
            order.Status = (int)Status.WithoutDriver;
            _repoOrder.Create(order);
        }

        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]Orders order)
        {
            if (order.IdDriver == id)
                _repoOrder.Update(order);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            Orders c = _repoOrder.FindById(id);
            Drivers driver = _repoDriver.FindById((Guid)c.IdDriver);
            driver.Status = 0;
            _repoOrder.Remove(c);
        }

        [HttpGet("calc")]
        public int Calculate(string From, string To)
        {
            return os.Calculate(From, To);
        }

        [HttpGet("CheckDenyDriver")]
        public string CheckRoadDriver(Guid id) //TODO 
        {
            Orders order = _repoOrder.FindById(id);
            if (order is null)
            {
                return "Водитель отказался от поездки";
            }
            else
            {
                if (order.Status == (int)Status.AwaitingClient)
                {
                    return "Водитель ожидает";
                }
                else
                {
                    return "Водитель в пути";
                }
            }
        }

        [HttpGet("CheckDenyClient")]
        public string CheckDenyClient(Guid id)  //TODO
        {
            Orders order = _repoOrder.FindById(id);
            if (order is null)
            {
                return "Клиент отказался от поездки";
            }
            else return null ;

        }

        [HttpGet("CheckDriver")]
        public Orders CheckDriver(Guid id)
        {
            var order = _repoOrder.Get().Where(dr => dr.IdDriver == id).First();
            if (order is null)
            {
                return null;
            } 
            else return order;
        }


        [HttpGet("CheckDriver")]
        public Drivers CheckClient(Guid id) //TODO
        {
            Orders order = _repoOrder.FindById(id);
            if (order.Status == 2)
            {
                return _repoDriver.FindById((Guid)order.IdDriver);
            }
            else return null;
        }

    }
}
