using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Enums
{
    public class StatusEnum
    {
        public enum Status
        {
            DriverFree = 0,
            DriverWithoutAgree = 1,
            DriverOnWay        = 2,
            AwaitingClient     = 3,
            ClientOnWay        = 4,
            Success            = 5
        }
    }
}
