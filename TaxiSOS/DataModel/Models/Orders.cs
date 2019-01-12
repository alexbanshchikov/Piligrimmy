using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Orders
    {
        public Guid IdOrder { get; set; }
        public Guid IdClient { get; set; }
        public Guid IdDriver { get; set; }
        public string ArrivalPoint { get; set; }
        public string DestinationPoint { get; set; }
        public DateTime OrderTime { get; set; }
        public int Cost { get; set; }
        public bool PayType { get; set; }
        public int Status { get; set; }

        public virtual Clients IdClientNavigation { get; set; }
        public virtual Drivers IdDriverNavigation { get; set; }
    }
}
