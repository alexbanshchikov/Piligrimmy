using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Clients
    {
        public Clients()
        {
            Cards = new HashSet<Cards>();
            Orders = new HashSet<Orders>();
        }

        public Guid IdClient { get; set; }
        public string TelephoneNumber { get; set; }
        public bool Blocked { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Email { get; set; }

        public virtual Account TelephoneNumberNavigation { get; set; }
        public virtual ICollection<Cards> Cards { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
