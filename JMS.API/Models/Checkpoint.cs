using System;
using System.Collections.Generic;

namespace JMS.API.Models
{
    public partial class Checkpoint
    {
        public Checkpoint()
        {
            JourneyUpdate = new HashSet<JourneyUpdate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }

        public virtual ICollection<JourneyUpdate> JourneyUpdate { get; set; }
    }
}
