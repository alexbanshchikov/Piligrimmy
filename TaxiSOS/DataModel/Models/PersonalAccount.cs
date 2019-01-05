using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class PersonalAccount
    {
        public Guid IdDriver { get; set; }
        public string AccountNumber { get; set; }
        public int Balance { get; set; }

        public virtual Drivers IdDriverNavigation { get; set; }
    }
}
