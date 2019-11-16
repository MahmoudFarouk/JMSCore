using JMS.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMS.API.Models
{
    public class JourneyUpdateModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? JourneyId { get; set; }
        public JourneyStatus JourneyStatus { get; set; }
        public string VehicleNo { get; set; }
        public string DriverId { get; set; }
        public bool IsJourneyCheckpoint { get; set; }
        public int? CheckpointId { get; set; }
        public Level RiskLevel { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsDriverStatus { get; set; }
        public bool IsAlert { get; set; }
        public string StatusMessage { get; set; }
    }
}
