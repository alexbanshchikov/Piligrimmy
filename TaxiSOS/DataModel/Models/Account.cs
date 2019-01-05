using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Account
    {
        public Account()
        {
            Clients = new HashSet<Clients>();
            Drivers = new HashSet<Drivers>();
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Clients> Clients { get; set; }
        public virtual ICollection<Drivers> Drivers { get; set; }
    }
}
