using System;
using System.Collections;
using System.Collections.Generic;

namespace JMS.DAL.Models
{
    public partial class Checkpoint
    {
        public Checkpoint()
        {
            JourneyUpdate = new HashSet<JourneyUpdate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsThirdParty { get; set; }


        public virtual ICollection<JourneyUpdate> JourneyUpdate { get; set; }

    }
}
