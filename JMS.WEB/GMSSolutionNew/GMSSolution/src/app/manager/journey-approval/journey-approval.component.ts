import { Component, OnInit } from '@angular/core';
import { Subscription, Observable, timer } from 'rxjs';
import { ServiceResponse } from '../../shared/models/ServiceResponseModel';
import { JourneyService } from './../../shared/Services/JourneyService';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { JourneyModel } from '../../shared/models/JourneyModel';
import { JourneyStatus } from '../../shared/enums/journey-status.enum';
import { QuestionCategory } from '../../shared/enums/question-category.enum';
import { User } from '../../shared/models/UserModel';
import { AuthenticationService } from '../../shared/Services/AuthenticationService';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-journey-approval',
  templateUrl: './journey-approval.component.html',
  styleUrls: ['./journey-approval.component.css']
})
export class JourneyApprovalComponent implements OnInit {
  private JourneyId: number = 0;
  currentUser: User;
  constructor(private JourneyService: JourneyService,
    private activatedRoute: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private router: Router) {
    this.currentUser = this.authenticationService.currentUserValue;
  }
  AllAssesments: any[];
  VechileCheckList: any[];
  PretripAssessmeent: any[];
  PosTripAssessment: any[];
  CheckPointAssessment: any[] = [];
  data: JourneyModel = {
    "id": 0,
    "title": null,
    "isTruckTransport": false,
    "journeyStatus": 0,
    "fromDestination": null,
    "fromLat": 0.0,
    "fromLng": 0.0,
    "toDestination": null,
    "toLat": null,
    "toLng": null,
    "startDate": null,
    "deliveryDate": null,
    "cargoWeight": null,
    "cargoPriority": 0,
    "cargoSeverity": 0,
    "cargoType": null,
    "userId": "00000000-0000-0000-0000-000000000000",
    "userFullname": null,
    "isThirdParty": false,
    assesments: []
  };
  assessments: any[] = [];
  journey: ServiceResponse<JourneyModel> = {
    data: this.data,
    status: 1,
    exception: null,
    message: ""
  }
  RiskStatus = '';
  showVechileCheckList() {
    return this.showDriverAssessment() || this.showCheckPoint() || this.showpostTrip();
  }
  showDriverAssessment() {

    if (this.currentUser.roles.length) {
      var userRole = this.currentUser.roles[0].name;
      return (this.journey.data.journeyStatus == JourneyStatus.PendingOnJMCApproveDriverPreTripAssessment && userRole == "JMC")
        || (this.journey.data.journeyStatus == JourneyStatus.PendingOnGBMJourneyApprovalPreTripAssessment && userRole == "GBM")
        || (this.journey.data.journeyStatus == JourneyStatus.PendingOnQHSEJourneyApprovalPreTripAssessment && userRole == "QHSE")
    }
    return false
  }
  showCheckPoint() {
    var userRole = this.currentUser.roles[0].name;
    return (this.journey.data.journeyStatus == JourneyStatus.PendingOnJMCApproveDriverCheckpointAssessment && userRole == "JMC")
  }
  showpostTrip() {
    var userRole = this.currentUser.roles[0].name;
    return (this.journey.data.journeyStatus == JourneyStatus.PendingOnJMCApproveDriverPostTripAssessment && userRole == "JMC")
      || (this.journey.data.journeyStatus == JourneyStatus.PendingOnDispatcherApproveDriverPostTripAssessment && userRole == "Dispatcher")


  }

  showApprove() {

    if (this.currentUser.roles.length) {
      var userRole = this.currentUser.roles[0].name;
      return (this.journey.data.journeyStatus == JourneyStatus.PendingOnJMCApproveDriverCheckpointAssessment && userRole == "JMC")
        || (this.journey.data.journeyStatus == JourneyStatus.PendingOnJMCApproveDriverPostTripAssessment && userRole == "JMC")
        || (this.journey.data.journeyStatus == JourneyStatus.PendingOnDispatcherApproveDriverPostTripAssessment && userRole == "Dispatcher")
    }
    return false
  }
  getchekPointName() {
    return this.CheckPointAssessment.length > 0 ? this.CheckPointAssessment[0].checkpoint.name : '';
  }
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      const journeyId = params['journeyId'];
      this.JourneyId = journeyId != undefined && journeyId != null && journeyId != '' ? journeyId : 0;

    });
    this.JourneyService.GetJourneyDetails(this.JourneyId).toPromise().then((data: any) => {

      if (data.status = 1) {
        this.journey = data;
        var status = this.journey.data.journeyStatus;
        switch (status) {
          case JourneyStatus.PendingOnJMCApproveDriverPreTripAssessment:
            this.AllAssesments = this.journey.data.assesments;
            debugger;
            this.PretripAssessmeent = this.AllAssesments.filter(x => x.category == QuestionCategory.PreTrip);
            this.VechileCheckList = this.AllAssesments.filter(x => x.category == QuestionCategory.VehicleChecklist);
            var sumyes = 0;
            var sumNo = 0;
            for (var i in this.AllAssesments) {
              var item = this.AllAssesments[i];
              var result = item.assessmentResult.length > 0 ? item.assessmentResult[0] : [];
              if (result.length > 0) {
                if (result.isYes)
                  sumyes += 1;
                else
                  sumNo += 1;
              }
            }
            var total = sumyes + sumNo;
            var yesPerctg = (sumyes / total) * 100;
            if (yesPerctg >= 80)
              this.RiskStatus = 'low';
            else if (yesPerctg < 80 && yesPerctg >= 60)
              this.RiskStatus = 'meduim';
            else
              this.RiskStatus = 'high'
            break;
          case JourneyStatus.PendingOnGBMJourneyApprovalPreTripAssessment:
            this.AllAssesments = this.journey.data.assesments;
            this.PretripAssessmeent = this.AllAssesments.filter(x => x.category == QuestionCategory.PreTrip);
            this.VechileCheckList = this.AllAssesments.filter(x => x.category == QuestionCategory.VehicleChecklist);

            break;
          case JourneyStatus.PendingOnQHSEJourneyApprovalPreTripAssessment:
            this.AllAssesments = this.journey.data.assesments;
            this.PretripAssessmeent = this.AllAssesments.filter(x => x.category == QuestionCategory.PreTrip);
            this.VechileCheckList = this.AllAssesments.filter(x => x.category == QuestionCategory.VehicleChecklist);

            break;
          case JourneyStatus.PendingOnJMCApproveDriverCheckpointAssessment:
            this.AllAssesments = this.journey.data.assesments;
            debugger;
            this.CheckPointAssessment = this.AllAssesments.filter(x => x.category == QuestionCategory.CheckpointAssessment);
            this.VechileCheckList = this.AllAssesments.filter(x => x.category == QuestionCategory.VehicleChecklist);
            console.log(this.VechileCheckList)
            break;
          case JourneyStatus.PendingOnJMCApproveDriverPostTripAssessment:
            this.AllAssesments = this.journey.data.assesments;
            this.PosTripAssessment = this.AllAssesments.filter(x => x.category == QuestionCategory.PostTrip);
            this.VechileCheckList = this.AllAssesments.filter(x => x.category == QuestionCategory.VehicleChecklist);

            break;
          case JourneyStatus.PendingOnDispatcherApproveDriverPostTripAssessment:
            this.AllAssesments = this.journey.data.assesments;
            this.PosTripAssessment = this.AllAssesments.filter(x => x.category == QuestionCategory.PostTrip);
            this.VechileCheckList = this.AllAssesments.filter(x => x.category == QuestionCategory.VehicleChecklist);

            break;
        }
      }

    }, (error) => {

    });

  }
  changeDriver() {
    this.router.navigate(['/driver-selection'], { queryParams: { journeyId: this.JourneyId } });
  }
  approveDriver() {
    var status = this.journey.data.journeyStatus;
    debugger;
    switch (status) {
      case JourneyStatus.PendingOnJMCApproveDriverPreTripAssessment:

        if (this.RiskStatus == 'low') {
          this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.PendingOnDriverStartJourney).toPromise().then((data) => {
            debugger;
            Swal.fire('', 'Driver approved successfully', 'success');
            this.router.navigate(['/']);
          }, (error) => {

          });
        } else if (this.RiskStatus == 'meduim') {
          this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.PendingOnGBMJourneyApprovalPreTripAssessment).toPromise().then((data) => {
            debugger;
            Swal.fire('', 'Driver approved successfully', 'success');
            this.router.navigate(['/']);
          }, (error) => {

          });
        } else if (this.RiskStatus == 'high') {
          this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.PendingOnQHSEJourneyApprovalPreTripAssessment).toPromise().then((data) => {
            debugger;
            Swal.fire('', 'Driver approved successfully', 'success');
            this.router.navigate(['/']);
          }, (error) => {

          });
        }
        break;
      case JourneyStatus.PendingOnGBMJourneyApprovalPreTripAssessment:
        this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.PendingOnDriverStartJourney).toPromise().then((data) => {
          debugger;
          Swal.fire('', 'Driver approved successfully', 'success');
          this.router.navigate(['/']);
        }, (error) => {

        });
        break;
      case JourneyStatus.PendingOnQHSEJourneyApprovalPreTripAssessment:
        this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.PendingOnDriverStartJourney).toPromise().then((data) => {
          debugger;
          Swal.fire('', 'Driver approved successfully', 'success');
          this.router.navigate(['/']);
        }, (error) => {

        });
        break;
    }
  }
  approve() {
    var status = this.journey.data.journeyStatus;
    switch (status) {

      case JourneyStatus.PendingOnJMCApproveDriverCheckpointAssessment:
        /////////////////////
        this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.PendingOnDriverCompleteCheckpointAssessment).toPromise().then((data) => {
          debugger;
          Swal.fire('', 'Your checkpoint approved successfully', 'success');
          this.router.navigate(['/']);
        }, (error) => {

        });
        ////////////////////////////
        break;
      case JourneyStatus.PendingOnJMCApproveDriverPostTripAssessment:
        this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.JourneyCompleted).toPromise().then((data) => {
          debugger;
          Swal.fire('', 'Journey completed successfully', 'success');
          this.router.navigate(['/']);
        }, (error) => {

        });
        break;
      case JourneyStatus.PendingOnDispatcherApproveDriverPostTripAssessment:
        this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.JourneyCompleted).toPromise().then((data) => {
          debugger;
          Swal.fire('', 'Journey completed successfully', 'success');
          this.router.navigate(['/']);
        }, (error) => {

        });
        break;
    }
  }
  getGourneyStatus(num) {
    var tex = "";
    switch (num) {
      case 0:
        tex = "Pending On Approval";
        break;
      case 1:
        tex = "Approved";
        break;
      case 2:
        tex = "Canceled";
        break;
      case 3:
        tex = "Stopped";
        break;
      case 4:
        tex = "Completed";
        break;
    }
    return tex;
  }
  getLevelStatus(num) {

    var tex = "";
    switch (num) {
      case 0:
        tex = "Low";
        break;
      case 1:
        tex = "Medium";
        break;
      case 2:
        tex = "High";
        break;

    }
    return tex;
  }
  getQuestionCategory(num) {

    var tex = "";
    switch (num) {
      case 0:
        tex = "Checkpoint Assessment";
        break;
      case 1:
        tex = "Vehicle Checklist";
        break;
      case 2:
        tex = "PreTrip";
        break;
      case 3:
        tex = "PostTrip";
        break;
    }
    return tex;
  }
}
