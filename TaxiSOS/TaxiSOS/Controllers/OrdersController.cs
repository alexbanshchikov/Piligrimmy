using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TaxiSOS.Services;
using DataModel;
using DataModel.Models;
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
        //  public void Create([FromBody]Orders order)
        public void Create(Guid id,string arrivalPoint, string destinationPoint)
        {
            Orders order = new Orders();
            order.IdClient = id;
            order.ArrivalPoint = arrivalPoint;
            order.DestinationPoint = destinationPoint;
            order.OrderTime = DateTime.Now;
            order.Cost = Calculate(order.ArrivalPoint, order.DestinationPoint);
            order.IdDriver = os.FindDriver(_repoDriver);
            order.Status = (int)Status.DriverWithoutAgree;
            _repoOrder.Create(order);
        }

        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]Orders order)
        {
            if (order.IdOrder == id)
                _repoOrder.Update(order);
        }

        [HttpGet("delete")]
        public void Delete(Guid id)
        {
            var order = _repoOrder.Get().Where(or => or.IdClient == id && or.Status !=5).First();
            Drivers driver = _repoDriver.FindById(order.IdDriver);
            driver.Status = 0;
            _repoDriver.Update(driver);
            _repoOrder.Remove(order);
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
            string _From = string.Join("%20", From.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            string _To =   string.Join("%20", To.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            return os.Calculate(_From, _To);
        }

        [HttpGet("GetCoordinates")]
        public string[] GetCoordinates(string From, string To)
        {
            string[] coordinates = new string[4];

            string From_ = string.Join("%20", From.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            string To_ = string.Join("%20", To.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            os.Points(From_, out coordinates[0], out coordinates[1]);
            os.Points(To_, out coordinates[2], out coordinates[3]);

            return coordinates;
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
        public string CheckDenyClient(Guid idOrder, Guid idDriver)
        {
            Orders order = _repoOrder.FindById(idOrder);
            if (order is null)
            {
                var driver = _repoDriver.FindById(idDriver);
                driver.Status = 0;
                _repoDriver.Update(driver);
                return "Клиент отказался от поездки";
            }
            else return null;
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
                return _repoDriver.FindById(order.IdDriver);
        }


        [HttpGet("GetAllDriverOrder")]
        public IEnumerable<Orders> GetAllDriverOrder(Guid idDriver)
        {
            var order = _repoOrder.Get().Where(dr => dr.IdDriver == idDriver && dr.Status == (int)Status.Success);
            return order;
        }

        /// <summary>
        /// Действие водителя: Проверяет, назначен ли он на заказ
        /// </summary>
        /// <param Id водителя="idDriver"></param>
        /// <returns>Актуальный заказ</returns>
        [HttpGet("CheckClient")]
        public Orders CheckClient(Guid idDriver)
        {
            try
            {
                var order = _repoOrder.Get().Where(dr => dr.IdDriver == idDriver && dr.Status == (int)Status.DriverWithoutAgree).First();
                order.Status = 6;
                _repoOrder.Update(order);
                return order;                
            }
            catch
            {
                return null;
            }

        }

        [HttpGet("DriverIgnore")]
        public void DriverIgnore(Guid idOrder)
        {
            var order = _repoOrder.FindById(idOrder);
            var driver = _repoDriver.FindById(order.IdDriver);
            order.IdDriver = os.FindDriver(_repoDriver);
            driver.Status = 0;
            _repoDriver.Update(driver);
            _repoOrder.Update(order);
        }

        /// <summary>
        /// Смена статуса заказа
        /// #Водитель принял заказ
        /// </summary>
        /// <param name="newStatus"></param>
        /// <param name="idOrder"></param>
        [HttpGet("ChangeStatus")]
        public void ChangeStatus(int newStatus, Guid idOrder)
        {
            var order = _repoOrder.FindById(idOrder);
            order.Status = newStatus;
            _repoOrder.Update(order);
        }


        [HttpGet("ChangeStatusDriver")]
        public void ChangeStatusDriver(int newStatus, Guid idDriver)
        {
            var driver = _repoDriver.FindById(idDriver);
            driver.Status = newStatus;
            _repoDriver.Update(driver);
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
