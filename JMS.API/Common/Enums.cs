using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMS.API.Common
{
    public class Enums
    {
        public enum ResponseStatus
        {
            ServerError = 0,
            Success = 1,
            Failed = 2,
            Unauthorized = 3,
            Expired = 4
        }
    }
}
