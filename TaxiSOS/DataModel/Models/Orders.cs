using System;
using System.Collections.Generic;

namespace DataModel
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

        public Clients IdClientNavigation { get; set; }
        public Drivers IdDriverNavigation { get; set; }
    }
}
