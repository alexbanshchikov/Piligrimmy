using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Cars
    {
        public Guid IdCar { get; set; }
        public Guid IdDriver { get; set; }
        public string RegistrationNumber { get; set; }
        public string Mark { get; set; }
        public string Color { get; set; }
        public string ServiceClass { get; set; }

        public Drivers IdDriverNavigation { get; set; }
    }
}
