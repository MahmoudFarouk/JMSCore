import { Component, OnInit } from '@angular/core';
import { RequestService } from 'src/app/shared/Services/RequestsService';
import { RequestModel } from 'src/app/shared/Models/RequestModel';
import { Router } from '@angular/router';
import { JourneyStatus } from 'src/app/shared/enums/journey-status.enum';
import { AuthenticationService } from 'src/app/shared/Services/AuthenticationService';
import { User } from 'src/app/shared/models/UserModel';
import { UserRoles } from 'src/app/shared/Enums/user-roles.enum';

@Component({
  selector: 'app-my-requests',
  templateUrl: './my-requests.component.html',
  styleUrls: ['./my-requests.component.css']
})

export class MyRequestsComponent implements OnInit {

  currentUser: User;

  constructor(private requestService: RequestService, private router: Router, private authenticationService: AuthenticationService) {
    this.currentUser = this.authenticationService.currentUserValue;
  }
  requests = [];

  ngOnInit() {
    this.requestService.getRequests().subscribe(result => {
      this.requests = result;
    });;
  }

  navigateToRequest(request: RequestModel) {

    if (this.currentUser.roles[0].name == "JMC" || this.currentUser.roles[0].name == "Operation Manager" || this.currentUser.roles[0].name == "QHSE" || this.currentUser.roles[0].name == "GBM")
      switch (request.status) {
        case JourneyStatus.PendingOnJMCInitialApproval:
          this.router.navigate([`/validate-journey/${request.journeyId}`]);
          break;
        case JourneyStatus.DriverStartedJourney:
        case JourneyStatus.JourneyCompleted:
        case JourneyStatus.JourneyRejected:
          this.router.navigate([`/journey/${request.journeyId}`]);
          break;
        case JourneyStatus.PendingOnDriverSelection:
          this.router.navigate([`/driver-selection/`], { queryParams: { journeyId: request.journeyId } });
          break;
        case JourneyStatus.PendingOnJMCApproveDriverPreTripAssessment:
        case JourneyStatus.PendingOnGBMJourneyApprovalPreTripAssessment:
        case JourneyStatus.PendingOnQHSEJourneyApprovalPreTripAssessment:
        case JourneyStatus.PendingOnJMCApproveDriverCheckpointAssessment:
        case JourneyStatus.PendingOnJMCApproveDriverPostTripAssessment:
        case JourneyStatus.JourneyPaused:
        case JourneyStatus.JourneyStopped:
          this.router.navigate([`/journeyapproval/`], { queryParams: { journeyId: request.journeyId } });
          break;
      }

    if (this.currentUser.roles[0].name == "Dipsatcher")
      switch (request.status) {
        case JourneyStatus.PendingOnDispatcherApproval:
          this.router.navigate([`/validate-journey/${request.journeyId}`]);
          break;
        case JourneyStatus.PendingOnDriverSelection:
          this.router.navigate([`/driver-selection/`], { queryParams: { journeyId: request.journeyId } });
          break;
        case JourneyStatus.PendingOnDispatcherApproveDriverPostTripAssessment:
          this.router.navigate([`/journeyapproval/`], { queryParams: { journeyId: request.journeyId } });
          break;
      }

    if (this.currentUser.roles[0].name == "Driver")
      switch (request.status) {
        case JourneyStatus.PendingOnDriverCompletePreTripAssessment:
        case JourneyStatus.PendingOnDriverCompletePostTripAssessment:
        case JourneyStatus.PendingOnDriverCompleteCheckpointAssessment:
          this.router.navigate([`/driver/assessment/`], { queryParams: { journeyId: request.journeyId } });
          break;
        case JourneyStatus.PendingOnDriverStartJourney:
          this.router.navigate([`/driver/journey/`], { queryParams: { journeyId: request.journeyId } });
          break;
      }



  }
}
