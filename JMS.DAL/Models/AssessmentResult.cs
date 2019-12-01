using JMS.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMS.DAL.Models
{
    public partial class AssessmentResult
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public bool IsYes { get; set; }
        public string Comment { get; set; }
        
        public Guid UserId { get; set; }
        public int? VehicleNo { get; set; }
        public int? JourneyUpdateId { get; set; }
        public QuestionCategory? Category { get; set; }
        public int? CheckPointId { get; set; }

        public virtual JourneyUpdate JourneyUpdate { get; set; }
        public virtual AssessmentQuestion Question { get; set; }
        public virtual User User { get; set; }
    }
}
