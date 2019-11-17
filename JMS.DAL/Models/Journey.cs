using JMS.DAL.Common.Enums;
using System;
using System.Collections.Generic;

namespace JMS.DAL.Models
{
    public  class Journey
    {
        public Journey()
        {
            JourneyUpdate = new HashSet<JourneyUpdate>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsTruckTransport { get; set; }
        public JourneyStatus JourneyStatus { get; set; }
        public string FromDestination { get; set; }
        public double? FromLat { get; set; }
        public double? FromLng { get; set; }
        public string ToDistination { get; set; }
        public double? ToLat { get; set; }
        public double? ToLng { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public double? CargoWeight { get; set; }
        public Level CargoPriority { get; set; }
        public Level CargoSeverity { get; set; }
        public string CargoType { get; set; }
        public Guid UserId { get; set; }
        public bool IsThirdParty { get; set; }


        public virtual User User { get; set; }
        public virtual ICollection<JourneyUpdate> JourneyUpdate { get; set; }
    }
}
