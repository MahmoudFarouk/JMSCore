using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Models
{
    public class CalendarModel
    {
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime? end { get; set; }
        public string url { get; set; }
    }
}
