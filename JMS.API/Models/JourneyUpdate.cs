using System;
using System.Collections.Generic;

namespace JMS.API.Models
{
    public partial class JourneyUpdate
    {
        public JourneyUpdate()
        {
            AssessmentResult = new HashSet<AssessmentResult>();
        }

        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? JourneyId { get; set; }
        public int? Status { get; set; }
        public int? VehicleId { get; set; }
        public string DriverId { get; set; }
        public int? CheckpointId { get; set; }
        public int? RiskLevel { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual Checkpoint Checkpoint { get; set; }
        public virtual Journey Journey { get; set; }
        public virtual ICollection<AssessmentResult> AssessmentResult { get; set; }
    }
}
