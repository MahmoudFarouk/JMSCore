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
        [ForeignKey("User")]
        public Guid SubmittedBy { get; set; }
        public int? VehicleNo { get; set; }
        public int? JourneyUpdateId { get; set; }

        public virtual JourneyUpdate JourneyUpdate { get; set; }
        public virtual AssessmentQuestion Question { get; set; }
        public virtual User SubmittedByUser { get; set; }
    }
}
