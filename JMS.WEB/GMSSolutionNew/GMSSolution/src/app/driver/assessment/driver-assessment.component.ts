import { Component, OnInit } from '@angular/core';
import { Subscription, Observable, timer } from 'rxjs';
import { NgForm } from '@angular/forms';
import { AssessmentQuestion } from '../../shared/models/AssessmentQuestionModel';
import { AssessmentResult } from '../../shared/models/AssessmentResultModel';
import { JourneyModel } from '../../shared/models/JourneyModel';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { JourneyService } from './../../shared/Services/JourneyService';
import { General } from '../../shared/Helpers/General';
import { JourneyStatus } from '../../shared/enums/journey-status.enum';
import { DriverService } from '../../shared/Services/DriverService';
import { QuestionCategory } from '../../shared/enums/question-category.enum';
import swal from "sweetalert2";
import { AuthenticationService } from '../../shared/Services/AuthenticationService';
import { User } from '../../shared/models/UserModel';

@Component({
    selector: 'app-driver-assessment',
    templateUrl: './driver-assessment.component.html',
    styleUrls: ['./driver-assessment.component.css']
})
export class DriverAssessmentComponent implements OnInit {
    //loading: boolean = false;

    journeyId: number;
    checkPointId: number;
    journey: JourneyModel = {};
    pretripassessments: any[];
    posttripassessments: any[];
    checkpointassessments: any[];
    vechCheckList: any[];
    AllAssessments: any[];
    currentUser:User;
   
    constructor(private authenticationService: AuthenticationService,private JourneyService: JourneyService, private DriverService: DriverService, private activatedRoute: ActivatedRoute) {
        this.currentUser = this.authenticationService.currentUserValue;

    }
    ngOnInit() {
        this.activatedRoute.queryParams.subscribe(params => {
            const journeyId = params['journeyId'];
            const checkPointId = params['checkPointId'];
            this.journeyId = journeyId != undefined && journeyId != null && journeyId != '' ? journeyId : 0;
            this.checkPointId = checkPointId != undefined && checkPointId != null && checkPointId != '' ? checkPointId : 0;

        });

        this.getJourneyInfo()

    }
    showPretripAssessment() {
        return (this.journey.journeyStatus != null && this.journey.journeyStatus == JourneyStatus.PendingOnDriverCompletePreTripAssessment)

    }
    showPosttripAssessment() {
        return (this.journey.journeyStatus != null && this.journey.journeyStatus == JourneyStatus.PendingOnDriverCompletePostTripAssessment)

    }
    showCheckpointAssessment() {
        return (this.journey.journeyStatus != null && this.journey.journeyStatus == JourneyStatus.PendingOnDriverCompleteCheckpointAssessment)

    }
    showSubmit() {
        return this.showPretripAssessment() || this.showPosttripAssessment() || this.showCheckpointAssessment();
    }
    Submit() {
        var isvalid = this.validate();
        if (isvalid) {
            var data = [];
            for (var i in this.AllAssessments) {
                var item = this.AllAssessments[i];
                data.push({UserId:this.currentUser.id, QuestionId: item.id, IsYes: item.IsYes, Comment: '' });
            }
            var status: any = null;
            switch (this.journey.journeyStatus) {
                case JourneyStatus.PendingOnDriverCompletePreTripAssessment:
                    status = JourneyStatus.DriverCompletedPreTripAssessment
                    break;
                case JourneyStatus.PendingOnDriverCompletePostTripAssessment:
                    status = JourneyStatus.Completed
                    break;
                case JourneyStatus.PendingOnDriverCompleteCheckpointAssessment:
                    status = JourneyStatus.DriverCompletedCheckpointAssessemnt
                    break;

                default:
                    break;
            }
            if (data.length > 0 && status != null) {

                this.DriverService.SubmitAssessments(this.journeyId, status,data).toPromise().then((data) => {
                   this.journey.journeyStatus= null;
                })
            }
        }
    }
    validate() {

        var isNotValidToSubmit = false;
        for (var i in this.AllAssessments) {
            if (this.AllAssessments[i].IsYes == undefined)
                isNotValidToSubmit = true;
        }
        if (!isNotValidToSubmit)
            return true;
        swal.fire("", "You must select all question Answers", "warning");
        return false;


    }
    SelectAnswer(item, isyes) {
        item.IsYes = isyes;
    }
    getJourneyInfo() {
        this.JourneyService.GetJourneyInfo(this.journeyId).toPromise().then((data: any) => {
            this.journey = data.data;

            this.getpretripassessment();
            this.getPostTripAssessment();
            this.getCheckPointAssessment();
        }, (error) => {
        });
    }
    getpretripassessment() {
        if (this.journey.journeyStatus == JourneyStatus.PendingOnDriverCompletePreTripAssessment)
            this.DriverService.GetPretripAssessment(this.journeyId).toPromise().then((data: any) => {
                this.AllAssessments = data.data
                this.pretripassessments = this.AllAssessments.filter(x => x.category == QuestionCategory.PreTrip);
                this.vechCheckList = this.AllAssessments.filter(x => x.category == QuestionCategory.VehicleChecklist);

            }, (error) => {
            });
    }
    getPostTripAssessment() {
        if (this.journey.journeyStatus == JourneyStatus.PendingOnDriverCompletePostTripAssessment)
            this.DriverService.GetPostTripAssessment(this.journeyId).toPromise().then((data: any) => {
                this.AllAssessments = data.data
                this.posttripassessments = this.AllAssessments.filter(x => x.category == QuestionCategory.PostTrip);
                this.vechCheckList = this.AllAssessments.filter(x => x.category == QuestionCategory.VehicleChecklist);

            }, (error) => {
            });
    }
    getCheckPointAssessment() {
        if (this.journey.journeyStatus == JourneyStatus.PendingOnDriverCompleteCheckpointAssessment && this.checkPointId > 0)
            this.DriverService.GetCheckpointAssessment(this.checkPointId).toPromise().then((data: any) => {
                this.AllAssessments = data.data
                this.checkpointassessments = this.AllAssessments.filter(x => x.category == QuestionCategory.CheckpointAssessment);
                this.vechCheckList = this.AllAssessments.filter(x => x.category == QuestionCategory.VehicleChecklist);
                debugger;
            }, (error) => {
            });
    }
    // GetPreTripAssessment() {
    //     this.userService.GetPreTripAssessment(this.journeyId).subscribe({
    //         next: (result: any) => {
    //             let response = result as AssessmentQuestion[];
    //             if (!response || response.length == 0) {
    //                 this.setErrorFlags();
    //                 return null;
    //             }
    //             this.preTripAssessmentQuestionList = response;
    //             this.loading = false;
    //         },
    //         error: err => {
    //             this.setErrorFlags();
    //         }
    //     });
    // }
    // GetCheckPointAssessment() {
    //     this.userService.GetCheckPointAssessment(this.checkpointid).subscribe({
    //         next: (result: any) => {
    //             let response = result as AssessmentQuestion[];
    //             if (!response || response.length == 0) {
    //                 this.setErrorFlags();
    //                 return null;
    //             }
    //             this.checkPointAssessmentQuestionList = response;
    //             this.loading = false;
    //         },
    //         error: err => {
    //             this.setErrorFlags();
    //         }
    //     });
    // }
    // GetPostTripAssessment() {
    //     this.userService.GetPostTripAssessment(this.journeyId).subscribe({
    //         next: (result: any) => {
    //             let response = result as AssessmentQuestion[];
    //             if (!response || response.length == 0) {
    //                 this.setErrorFlags();
    //                 return null;
    //             }
    //             this.postTripAssessmentQuestionList = response;
    //             this.loading = false;
    //         },
    //         error: err => {
    //             this.setErrorFlags();
    //         }
    //     });
    // }
    // checkAssessmentResult(event) {

    // }

    // setErrorFlags() {
    //     this.loading = false;
    //     this.isSuccessMode = false;
    //     this.isErrorMode = true;
    // }
    // setLoadingAssessmentsErrorFlags() {
    //     this.errorMessage = "An Error Occured during loading assessment";
    //     this.loading = false;
    //     this.isSuccessMode = false;
    //     this.isErrorMode = true;
    // }
    // setSubmitAssessmentErrorFlags() {
    //     this.errorMessage = "An Error Occured during adding assessment";
    //     this.loading = false;
    //     this.isSuccessMode = false;
    //     this.isErrorMode = true;
    // }
    // resetForm(form?: NgForm) {
    //     if ((form = null)) {
    //         form.resetForm();
    //     }
    //    // this.formData = new AssessmentResult();
    // };
    // public setTimer() {
    //     this.loading = true;
    //     this.timer = timer(500);
    //     this.subscription = this.timer.subscribe(() => {
    //         this.loading = false;
    //     });
    // }
    // onSubmit(form: NgForm) {
    //     this.isSubmitted = true;
    //     if (!form.valid) {
    //         return;
    //     }
    //     if ((form.value.ID == null || form.value.ID === 0)) {
    //         this.userService.SubmitAssessment(form.value).subscribe({
    //             next: result => {
    //                 if (!result) {
    //                     this.setSubmitAssessmentErrorFlags();
    //                     return;
    //                 }
    //                 this.isSubmitted = false;
    //                 this.resetForm(form);
    //                 this.loading = false;
    //                 this.isSuccessMode = true;
    //                 this.errorMessage = "";
    //             },
    //             error: err => {
    //                 this.setSubmitAssessmentErrorFlags();
    //             }
    //         });
    //     } else {
    //         this.setSubmitAssessmentErrorFlags();
    //     }
    // }
}
