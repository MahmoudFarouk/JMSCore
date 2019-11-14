using System;
using System.Collections.Generic;

namespace JMS.DAL.Models
{
    public partial class AssessmentQuestion
    {
        public AssessmentQuestion()
        {
            AssessmentResult = new HashSet<AssessmentResult>();
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public int? Category { get; set; }
        public int? Weight { get; set; }
        public bool? IsThirdParty { get; set; }
        public int? CheckpointId { get; set; }

        public virtual Checkpoint Checkpoint { get; set; }

        public virtual ICollection<AssessmentResult> AssessmentResult { get; set; }
    }
}
