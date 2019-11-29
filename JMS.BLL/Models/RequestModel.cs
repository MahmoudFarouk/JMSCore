using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Models
{
    public class RequestModel
    {
        public int JourneyId { get; set; }
        public string JourneyTitle { get; set; }
        public string FromDestination { get; set; }
        public string ToDestination { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public JourneyStatus Status { get; set; }

    }
}
