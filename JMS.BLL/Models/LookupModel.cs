using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Models
{
    public class LookupModel<T>
    {
        public T Id { get; set; }
        public string Value { get; set; }
    }
}
