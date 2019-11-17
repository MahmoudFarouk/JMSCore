using JMS.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMS.DAL.Models
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
        public JourneyStatus JourneyStatus { get; set; }
        public string VehicleNo { get; set; }
        public string DriverId { get; set; }
        public bool IsJourneyCheckpoint { get; set; }
        public int? CheckpointId { get; set; }
        public Level RiskLevel { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsDriverStatus { get; set; }
        public bool IsAlert { get; set; }
        public string StatusMessage { get; set; }
        public Guid? UserId { get; set; }

        public virtual Checkpoint Checkpoint { get; set; }
        public virtual Journey Journey { get; set; }
        public virtual ICollection<AssessmentResult> AssessmentResult { get; set; }
        public virtual User User { get; set; }
    }
}
