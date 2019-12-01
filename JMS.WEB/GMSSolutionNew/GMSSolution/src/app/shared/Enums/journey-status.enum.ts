export enum JourneyStatus {

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
}
