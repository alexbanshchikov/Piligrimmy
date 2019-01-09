using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Clients
    {
        public Clients()
        {
            Orders = new HashSet<Orders>();
        }

        public Guid IdClient { get; set; }
        public string TelephoneNumber { get; set; }
        public string City { get; set; }
        public string Email { get; set; }

        public virtual Account TelephoneNumberNavigation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
