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
        OldPasswordWrong = 5,
        UsernameNotExsit = 6
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
        Driver = 3,
        QHSE = 4,
        GBM = 5,
        OperationManager = 6
    }

    public enum Level
    {
        Low = 0,
        Medium = 1,
        High = 2
    }

    public enum JourneyStatus
    {
        PendingOnJMCInitialApproval = 0,
        PendingOnDispatcherApproval = 1,

        PendingOnDriverSelection = 2,

        PendingOnDriverCompletePreTripAssessment = 3,

        //Journey Approval
        PendingOnJMCApproveDriverPreTripAssessment = 4, //Risk Status Low, High, Medium
        PendingOnGBMJourneyApprovalPreTripAssessment = 5, //Risk Status Medium
        PendingOnQHSEJourneyApprovalPreTripAssessment = 6, //Risk Status High

        JourneyRejected = 7,

        PendingOnDriverStartJourney = 8,
        DriverStartedJourney = 9,
        PendingOnDriverCompleteCheckpointAssessment = 10,
        PendingOnJMCApproveDriverCheckpointAssessment = 11,

        JourneyPaused = 12,
        JourneyStopped = 13,

        PendingOnDriverCompletePostTripAssessment = 14,

        PendingOnJMCApproveDriverPostTripAssessment = 15,
        PendingOnDispatcherApproveDriverPostTripAssessment = 16,

        JourneyCompleted = 17,
        JourneyClosed=18
    }

}
