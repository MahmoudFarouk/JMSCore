using JMS.BLL.Common;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Interfaces
{
    public interface ICheckpointService
    {
        public ServiceResponse<List<Checkpoint>> GetCheckpoints(double startLat, double startLng, double endLat, double endLng, bool isThirdParty = false);

        public ServiceResponse AddCheckpoint(Checkpoint checkpoint);

        public ServiceResponse UpdateCheckpoint(Checkpoint checkpoint);

        public ServiceResponse DeleteCheckpoint(int checkpointId);
<<<<<<< HEAD

        public ServiceResponse<List<JourneyUpdate>> GetCheckpointsByJourneyId(int journeyId);






=======
>>>>>>> b79b7c92752be7bacd4d5d96c2562d7892011fe8
    }
}
