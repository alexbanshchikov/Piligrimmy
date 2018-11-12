using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Enums
{
    public class StatusEnum
    {
        public enum Status
        {
            WithoutDriver      = 1,
            DriverWithoutAgree = 2,
            DriverOnWay        = 3,
            AwaitingClient     = 4,
            ClientOnWay        = 5,
            Success            = 6
        }
    }
}
