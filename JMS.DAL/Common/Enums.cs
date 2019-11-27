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
        UsernameNotExsit=6
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
        PendingOnDispatcherApproval = 2,

        PendingOnDriverSelection = 3,
        JMCAssignedDriver,
        DispatcherAssignedDriver,

        PendingOnDriverCompletePreTripAssessment,
        DriverCompletedPreTripAssessment,
        PendingOnJMCApproveDriverPreTripAssessment,
        JMCApprovedDriver,
        JMCChangedDriver,

        PendingOnJMCJourneyApproval,
        JMCApprovedJourney,
        JMCRejectedJourney,
        PendingOnQHSEJourneyApproval = 4,
        QHSEApprovedJourney,
        QHSEejectedJourney,
        PendingOnGBMJourneyApproval = 5,
        JBMApprovedJourney,
        GBMRejectedJourney,

        PendingOnDriverStartJourney,
        DriverStartedJourney,

        DriverProceedNextCheckpoint,
        DriverReachedCheckpoint,
        PendingOnDriverCompleteCheckpointAssessment,
        DriverCompletedCheckpointAssessemnt,

        PendingOnJMCCheckpointApproval,
        JMCApprovedCheckpoint,
        JMCRejectedCheckpoint,
        PendingOnQHSECheckpointApproval = 4,
        QHSEApprovedCheckpoint,
        QHSERejectedCheckpoint,
        PendingOnGBMCheckpointApproval = 5,
        GBMApprovedCheckpoint,
        GBMRejectedCheckpoint,

        PendingOnDriverCompletePostTripAssessment,
        Completed,
        Closed ,
        Paused = 9,
        Stopped =  8,
    }

}
