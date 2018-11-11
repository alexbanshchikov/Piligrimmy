using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Enums
{
    public class StatusEnum
    {
        enum Status
        {
            WithoutDriver = 1,
            DriverOnWay,
            AwaitingClient,
            ClientOnWay,
            Success
        }
    }
}
