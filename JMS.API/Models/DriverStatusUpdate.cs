using System;
using System.Collections.Generic;

namespace JMS.API.Models
{
    public partial class DriverStatusUpdate
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string DriverId { get; set; }
        public int? JourneyId { get; set; }

        public virtual AspNetUsers Driver { get; set; }
        public virtual Journey Journey { get; set; }
    }
}
