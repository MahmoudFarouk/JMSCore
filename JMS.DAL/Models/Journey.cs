using JMS.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMS.DAL.Models
{
    public class Journey
    {
        public Journey()
        {
            JourneyUpdates = new HashSet<JourneyUpdate>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsTruckTransport { get; set; }
        public JourneyStatus JourneyStatus { get; set; }
        public string FromDestination { get; set; }
        public double? FromLat { get; set; }
        public double? FromLng { get; set; }
        public string ToDestination { get; set; }
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
        public Guid? DispatcherId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsNight { get; set; }
        public RiskStatus? RiskStatus { get; set; }
        public string RecjectReason { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("DispatcherId")]
        [InverseProperty("DispatcherJourneys")]
        public virtual User Dispatcher { get; set; }
        public virtual ICollection<JourneyUpdate> JourneyUpdates { get; set; }
        public virtual ICollection<AssessmentQuestion> AssessmentQuestion { get; set; }

        [NotMapped]
        public virtual ICollection<Checkpoint> Checkpoints { get; set; }
    }
    public enum RiskStatus
    {
        Low,
        Meduim,
        High
    }
}
