using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Cars
    {
        public Guid IdCar { get; set; }
        public Guid IdDriver { get; set; }
        public string RegistrationNumber { get; set; }
        public string Mark { get; set; }
        public string Color { get; set; }
        public string ServiceClass { get; set; }

        public virtual Drivers IdDriverNavigation { get; set; }
    }
}
