using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMS.DAL.Common.Enums
{

    public enum ResponseStatus
    {
        ServerError = 0,
        Success = 1,
        Failed = 2,
        Unauthorized = 3,
        Expired = 4,
        OldPasswordWrong=5
    }

    public enum QuestionCategory
    {
        CheckpointAssessment = 0,
        VehicleChecklist = 1,
        PreTrip = 2,
        PostTrip = 3

    }

    public enum UserRoles
    {
        ProductLine = 0,
        Dispatcher = 1,
        JMC = 2,
        Driver = 3
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
