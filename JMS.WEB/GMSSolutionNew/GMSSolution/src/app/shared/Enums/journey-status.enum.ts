export enum JourneyStatus {

  PendingOnJMCInitialApproval = 0,
  PendingOnDispatcherApproval = 2,

  PendingOnDriverSelection = 3,
  JMCAssignedDriver=1,
  DispatcherAssignedDriver=4,

  PendingOnDriverCompletePreTripAssessment = 6,
  DriverCompletedPreTripAssessment=5,
  PendingOnJMCApproveDriverPreTripAssessment=7,
  JMCApprovedDriver=8,
  JMCChangedDriver=9,

  PendingOnJMCJourneyApproval=10,
  JMCApprovedJourney=11,
  JMCRejectedJourney=13,
  PendingOnQHSEJourneyApproval = 21,
  QHSEApprovedJourney=15,
  QHSEejectedJourney=16,
  PendingOnGBMJourneyApproval = 17,
  JBMApprovedJourney=18,
  GBMRejectedJourney=19,

  PendingOnDriverStartJourney=20,
  DriverStartedJourney=22,

  DriverProceedNextCheckpoint=23,
  DriverReachedCheckpoint=24,
  PendingOnDriverCompleteCheckpointAssessment = 12,
  DriverCompletedCheckpointAssessemnt=25,

  PendingOnDriverCompletePostTripAssessment = 14,
  Completed=26,
  Closed=27,
  Paused = 28,
  Stopped = 29,
  PendingOnDispatcherCompletion=30,
  PendingOnJMCApproval=31,
  Approved=32,
  Canceled=33
}
