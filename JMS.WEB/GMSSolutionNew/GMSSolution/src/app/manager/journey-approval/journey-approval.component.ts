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
import { General } from 'src/app/shared/Helpers/General';
declare var $: any;
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
    riskStatus: 0,
    isNight: false,
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
        || (this.journey.data.journeyStatus == JourneyStatus.PendingOnDriverStartJourney && userRole == "QHSE")
        || (this.journey.data.journeyStatus == JourneyStatus.PendingOnDriverStartJourney && userRole == "GBM")

      // || (this.journey.data.journeyStatus == JourneyStatus.DriverStartedJourney && userRole == "Operation Manager")

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

            this.PretripAssessmeent = this.AllAssesments.filter(x => x.category == QuestionCategory.PreTrip);
            this.VechileCheckList = this.AllAssesments.filter(x => x.category == QuestionCategory.VehicleChecklist);
            /* var sumyes = 0;
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
               this.RiskStatus = 'high'*/
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
    
    console.log(this.journey.data);
    if (this.journey.data.riskStatus == null && this.journey.data.isNight == null) {
      Swal.fire('', 'Please, Select Risk status or select is night driving ', 'warning');

      return;
    }
    switch (status) {
      case JourneyStatus.PendingOnJMCApproveDriverPreTripAssessment:

        if (this.journey.data.riskStatus == 0 || this.journey.data.riskStatus == 1) {
          this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.PendingOnDriverStartJourney).toPromise().then((data) => {
            this.JourneyService.UpdateJourneyRiskStatus(this.JourneyId, this.journey.data.isNight, this.journey.data.riskStatus).toPromise().then(() => {

              Swal.fire('Driver approved successfully', 'Notification Sent to the Driver to Start the Journey and Operation Manager is notified', 'success');
              this.router.navigate(['/']);
            });
          }, (error) => {

          });
        } /*else if (this.journey.data.riskStatus  == 1) {
          this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.PendingOnGBMJourneyApprovalPreTripAssessment).toPromise().then((data) => {
            
            Swal.fire('Driver approved successfully By JMC', 'Journey is Pending on GBM Approval', 'success');
            this.router.navigate(['/']);
          }, (error) => {

          });
        }*/ else if ((this.journey.data.riskStatus == 2 || this.journey.data.isNight) && this.journey.data.isTruckTransport) {
          this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.PendingOnDriverStartJourney).toPromise().then((data) => {
            this.JourneyService.UpdateJourneyRiskStatus(this.JourneyId, this.journey.data.isNight, this.journey.data.riskStatus).toPromise().then(() => {
              Swal.fire('Driver approved successfully By JMC', 'Notification Sent to the Driver to Start the Journey and QHSE Manager is notified', 'success');
              this.router.navigate(['/']);
            });
          }, (error) => {

          });
        } else if ((this.journey.data.riskStatus == 2 || this.journey.data.isNight) && !this.journey.data.isTruckTransport) {
          this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.PendingOnGBMJourneyApprovalPreTripAssessment).toPromise().then((data) => {
            this.JourneyService.UpdateJourneyRiskStatus(this.JourneyId, this.journey.data.isNight, this.journey.data.riskStatus).toPromise().then(() => {

              Swal.fire('Driver approved successfully By JMC', 'Journey is Pending on GBM Approval', 'success');
              this.router.navigate(['/']);
            });
          }, (error) => {

          });
        }
        break;
      case JourneyStatus.PendingOnGBMJourneyApprovalPreTripAssessment:
        this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.PendingOnDriverStartJourney).toPromise().then((data) => {
          this.JourneyService.UpdateJourneyRiskStatus(this.JourneyId, this.journey.data.isNight, this.journey.data.riskStatus).toPromise().then(() => {

            Swal.fire('Driver approved successfully', 'Notification Sent to the Driver to Start the Journey', 'success');
            this.router.navigate(['/']);
          });
        }, (error) => {

        });
        break;
      /*case JourneyStatus.PendingOnQHSEJourneyApprovalPreTripAssessment:
        this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.PendingOnDriverStartJourney).toPromise().then((data) => {

          Swal.fire('Driver approved successfully', 'Notification Sent to the Driver to Start the Journey', 'success');
          this.router.navigate(['/']);
        }, (error) => {

        });
        break;*/
    }

  }
  approve() {
    var status = this.journey.data.journeyStatus;
    switch (status) {

      case JourneyStatus.PendingOnJMCApproveDriverCheckpointAssessment:
        /////////////////////
        this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.PendingOnDriverCompleteCheckpointAssessment).toPromise().then((data) => {

          Swal.fire('', 'Your checkpoint approved successfully', 'success');
          this.router.navigate(['/']);
        }, (error) => {

        });
        ////////////////////////////
        break;
      case JourneyStatus.PendingOnJMCApproveDriverPostTripAssessment:
        this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.JourneyCompleted).toPromise().then((data) => {

          Swal.fire('', 'Journey completed successfully', 'success');
          this.router.navigate(['/']);
        }, (error) => {

        });
        break;
      case JourneyStatus.PendingOnDispatcherApproveDriverPostTripAssessment:
        this.JourneyService.UpdateJourneyStatus(this.JourneyId, JourneyStatus.JourneyCompleted).toPromise().then((data) => {

          Swal.fire('', 'Journey completed successfully', 'success');
          this.router.navigate(['/']);
        }, (error) => {

        });
        break;
    }
  }
  RejectJourney() {
    Swal.fire({
      title: '',
      text: "Are you sure to reject this trip",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, Reject it!'
    }).then((result) => {
      if (result.value) {
        $("#modalReason").click();
      }
    })
  }
  alertMessage: '';

  sendAlert() {
    if (this.alertMessage != undefined&&this.alertMessage != null&&this.alertMessage != '') {
      this.JourneyService.RejectJourney(this.JourneyId,this.alertMessage).toPromise().then((data) => {
        Swal.fire("", "Your trip has been rejected", "success").then(()=>{
          $("#closeAlert").click();
          this.router.navigate(['/']);
  
        });
      })

    } else {
      Swal.fire("", "Please, Enter your reason to reject this trip", "warning");

    }
  }
  getGourneyStatus(num) {
    return General.GetStatusName(num);
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

  getRiskStatus(num) {

    var tex = "";
    switch (num) {
      case 0:
        tex = "Low";
        break;
      case 1:
        tex = "Meduim";
        break;
      case 2:
        tex = "High";
        break;
    }
    return tex;
  }
  showSelectRiskStatusAndDrivernight() {
    return this.journey.data.journeyStatus == JourneyStatus.PendingOnJMCApproveDriverPreTripAssessment;
  }
  p
}
