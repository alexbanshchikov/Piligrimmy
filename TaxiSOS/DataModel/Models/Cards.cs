using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Cards
    {
        public Guid IdClient { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public DateTime ExpireDate { get; set; }

        public Clients IdClientNavigation { get; set; }
    }
}
