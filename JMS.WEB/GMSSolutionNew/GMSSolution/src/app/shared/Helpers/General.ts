import swal from "sweetalert2";
import { throwError } from 'rxjs';
import { JourneyStatus } from '../enums/journey-status.enum';
export class General{
    public static errorHandl(error) {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
            errorMessage = error.error.message;
        } else {
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }

        swal.fire("Error", "Sorry an error occured, Please try again or contact administration.", "error");
        console.log(errorMessage);
        return throwError(errorMessage);
    }
   public static GetStatusName(status:JourneyStatus){
    switch (status) {
        case JourneyStatus.PendingOnJMCInitialApproval:
          return "Pending On JMC Initial Approval";
        case JourneyStatus.PendingOnDispatcherApproval:
          return "Pending On Dispatcher Approval";
        case JourneyStatus.PendingOnDriverSelection:
          return "Pending On Driver Selection ";
        case JourneyStatus.PendingOnDriverCompletePreTripAssessment:
          return "Pending On Driver Complete PreTrip Assessment ";
        case JourneyStatus.PendingOnJMCApproveDriverPreTripAssessment:
          return "Pending On JMC Approve Driver PreTrip Assessment";
        case JourneyStatus.PendingOnGBMJourneyApprovalPreTripAssessment:
          return "Pending On GBM Journey Approval PreTrip Assessment ";
        case JourneyStatus.PendingOnQHSEJourneyApprovalPreTripAssessment:
          return "Pending On QHSE Journey Approval PreTrip Assessment ";
        case JourneyStatus.JourneyRejected:
          return "Journey Rejected ";
  
        case JourneyStatus.PendingOnDriverStartJourney:
          return "Pending On Driver Start Journey";
        case JourneyStatus.DriverStartedJourney:
          return "Driver Started Journey ";
        case JourneyStatus.PendingOnDriverCompleteCheckpointAssessment:
          return "Pending On Driver Complete Checkpoint Assessment";
        case JourneyStatus.PendingOnJMCApproveDriverCheckpointAssessment:
          return "Pending On JMC Approve Driver Checkpoint zaAssessment ";
        case JourneyStatus.PendingOnDriverCompletePostTripAssessment:
          return "Pending On Driver Complete PostTrip Assessment ";
        case JourneyStatus.PendingOnJMCApproveDriverPostTripAssessment:
          return "Pending On JMC Approve Driver Post Trip Assessment ";
        case JourneyStatus.PendingOnDispatcherApproveDriverPostTripAssessment:
          return "Pending On Dispatcher Approve Driver PostTrip Assessment ";
        case JourneyStatus.JourneyCompleted:
          return "Journey Completed ";
          case JourneyStatus.JourneyClosed:
            return "Journey Closed by Driver ";
        default:
          return "";
  
      }
   }
}