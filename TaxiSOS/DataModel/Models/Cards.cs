using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Cards
    {
        public Guid IdClient { get; set; }
        public string CardNumber { get; set; }
        public string CardOwner { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Cvv { get; set; }

        public virtual Clients IdClientNavigation { get; set; }
    }
}
