using System;
using System.Collections.Generic;

namespace DataModel.Models
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
        public string Name { get; set; }
        public bool Blocked { get; set; }
        public int Status { get; set; }

        public virtual Account LicenseNumberNavigation { get; set; }
        public virtual ICollection<Cars> Cars { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<PersonalAccount> PersonalAccount { get; set; }
    }
}
