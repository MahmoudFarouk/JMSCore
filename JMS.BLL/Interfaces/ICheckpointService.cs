using JMS.BLL.Common;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Interfaces
{
    interface ICheckpointService
    {
        public ServiceResponse<List<Checkpoint>> GetCheckpoints(string startLat, string startLng, string endLat, string endLng);

        public ServiceResponse AddCheckpoint(Checkpoint checkpoint);

        public ServiceResponse UpdateCheckpoint(Checkpoint checkpoint);

        public ServiceResponse DeleteCheckpoint(Checkpoint checkpoint);






    }
}
