using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IRepository<PersonalAccount> _repoPA = null;
        public OrdersController(IRepository<Drivers> repoDriver, IRepository<Orders> repoOrder, IRepository<PersonalAccount> repoPA)
        {
            _repoDriver = repoDriver;
            _repoOrder = repoOrder;
            _repoPA = repoPA;
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
            order.IdDriver = os.FindDriver(_repoDriver);
            order.Status = (int)Status.DriverWithoutAgree;
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

        /// <summary>
        /// Расчет стоимости поездки
        /// </summary>
        /// <param Адрес начального пункта="id"></param>
        /// <param Адрес конечного пункта="id"></param>
        /// <returns>Стоимость поездки</returns>
        [HttpGet("calc")]
        public int Calculate(string From, string To)
        {
            return os.Calculate(From, To);
        }

        /// <summary>
        /// Действие клиента: Проверяет отказ водителя от поездки или его статус
        /// </summary>
        /// <param Id заказа="idOrder"></param>
        /// <returns>Актуальный заказ</returns>
        [HttpGet("CheckDenyDriver")]
        public string CheckDenyDriver(Guid idOrder)
        {
            Orders order = _repoOrder.FindById(idOrder);
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

        /// <summary>
        /// Действие водителя: Следит, чтобы клиент не отказался от поездки
        /// </summary>
        /// <param Id заказа="idOrder"></param>
        /// <returns>Состояние клиента</returns>
        [HttpGet("CheckDenyClient")]
        public string CheckDenyClient(Guid idOrder)
        {
            Orders order = _repoOrder.FindById(idOrder);
            if (order is null)
            {
                return "Клиент отказался от поездки";
            }
            else return null ;
        }

        /// <summary>
        /// Действие клиента: Ожидает назначения водителя 
        /// </summary>
        /// <param Id заказа="idOrder"></param>
        /// <returns>Назначенный водитель</returns>
        [HttpGet("CheckDriver")]
        public Drivers CheckDriver(Guid idOrder)
        {
            var order = _repoOrder.FindById(idOrder);         
            if (order.Status == (int)Status.DriverWithoutAgree)
            {
                return null;
            } 
            else
                return _repoDriver.FindById((Guid)order.IdDriver);
        }

        /// <summary>
        /// Действие водителя: Проверяет, назначен ли он на заказ
        /// </summary>
        /// <param Id водителя="idDriver"></param>
        /// <returns>Актуальный заказ</returns>
        [HttpGet("CheckClient")]
        public Orders CheckClient(Guid idDriver)
        {
            var order = _repoOrder.Get().Where(dr => dr.IdDriver == idDriver && dr.Status == (int)Status.DriverWithoutAgree).First();
            if (order != null)
            {
                return order;
            }
            else return null;
        }

        [HttpGet("DriverIgnore")]
        public void DriverIgnore(Guid idOrder)
        {
            var order = _repoOrder.FindById(idOrder);
            order.IdDriver = os.FindDriver(_repoDriver);
            _repoOrder.Update(order);
        }

        [HttpGet("ChangeStatus")]
        public void ChangeStatus(int newStatus, Guid idOrder)
        {
            var order = _repoOrder.FindById(idOrder);
            order.Status = newStatus;
            _repoOrder.Update(order);
        }

        [HttpGet("TopUpBalance")]
        public void TopUpBalance(Guid idOrder, Guid idDriver)
        {
            var order = _repoOrder.FindById(idOrder);
            var account = _repoPA.FindById(idDriver);
            account.Balance += (int)(order.Cost * 0.75);
            _repoPA.Update(account);
        }
    }
}
