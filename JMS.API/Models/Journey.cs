using System;
using System.Collections.Generic;

namespace JMS.API.Models
{
    public partial class Journey
    {
        public Journey()
        {
            DriverStatusUpdate = new HashSet<DriverStatusUpdate>();
            JourneyUpdate = new HashSet<JourneyUpdate>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public bool? IsTruckTransport { get; set; }
        public string FromDestination { get; set; }
        public string FromLat { get; set; }
        public string FromLng { get; set; }
        public string ToDistination { get; set; }
        public string ToLat { get; set; }
        public string ToLng { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal? CargoWeight { get; set; }
        public int? CargoPriority { get; set; }
        public int? CargoSeverity { get; set; }
        public string CargoType { get; set; }
        public string DispatcherId { get; set; }

        public virtual AspNetUsers Dispatcher { get; set; }
        public virtual ICollection<DriverStatusUpdate> DriverStatusUpdate { get; set; }
        public virtual ICollection<JourneyUpdate> JourneyUpdate { get; set; }
    }
}
