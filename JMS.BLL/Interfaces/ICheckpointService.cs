using JMS.BLL.Common;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Interfaces
{
    interface ICheckpointService
    {
        public ServiceResponse<List<Checkpoint>> GetCheckpoints(string lat, string lng);

        public ServiceResponse AddCheckpoint(Checkpoint checkpoint);

        public ServiceResponse UpdateCheckpoint(Checkpoint checkpoint);

        public ServiceResponse DeleteCheckpoint(Checkpoint checkpoint);






    }
}
