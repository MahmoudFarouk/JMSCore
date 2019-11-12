﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JMS.API.Common.Enums;

namespace JMS.API.Common
{

    public class ServiceResponse
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; set; }
    }


}
