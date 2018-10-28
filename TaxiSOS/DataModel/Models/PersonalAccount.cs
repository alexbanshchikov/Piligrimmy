using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class PersonalAccount
    {
        public Guid IdDriver { get; set; }
        public string AccountNumber { get; set; }
        public int Balance { get; set; }

        public Drivers Drivers { get; set; }
    }
}
