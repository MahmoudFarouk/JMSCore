using System;
using System.Collections.Generic;

namespace JMS.DAL.Models
{
    public partial class AssessmentResult
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public bool? IsYes { get; set; }
        public string Comment { get; set; }
        public string SubmittedBy { get; set; }
        public int? VehicleId { get; set; }
        public int? JourneyUpdateId { get; set; }

        public virtual JourneyUpdate JourneyUpdate { get; set; }
        public virtual AssessmentQuestion Question { get; set; }
        public virtual AspNetUsers SubmittedByNavigation { get; set; }
    }
}
