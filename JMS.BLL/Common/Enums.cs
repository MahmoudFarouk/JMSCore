using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMS.BLL.Common
{
    public class Enums
    {
        public enum ResponseStatus
        {
            ServerError = 0,
            Success = 1,
            Failed = 2,
            Unauthorized = 3,
            Expired = 4
        }

        public enum QuestionCategory
        {
            CheckpointAssessment = 0,
            VehicleChecklist = 1,
            PreTrip = 2,
            PostTrip = 3

        }

        public enum CargoType
        {
            TruckGoods = 0,
            People = 1
        }

        public enum Level
        {
            Low = 0,
            Medium = 1,
            High = 2
        }

        public enum JourneyStatus
        {
            PendingOnApproval = 0,
            Approved = 1,
            Canceled = 2,
            Stopped = 3,
            Completed = 4
        }
    }
}
