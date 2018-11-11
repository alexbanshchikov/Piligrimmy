using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Drivers
    {
        public Drivers()
        {
            Cars = new HashSet<Cars>();
            Orders = new HashSet<Orders>();
            PersonalAccount = new HashSet<PersonalAccount>();
        }

        public Guid IdDriver { get; set; }
        public string LicenseNumber { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Blocked { get; set; }
        public int Status { get; set; }

        public ICollection<Cars> Cars { get; set; }
        public ICollection<Orders> Orders { get; set; }
        public ICollection<PersonalAccount> PersonalAccount { get; set; }
    }
}
