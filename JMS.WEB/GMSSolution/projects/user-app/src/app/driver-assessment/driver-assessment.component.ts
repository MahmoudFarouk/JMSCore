import { UserService } from './../../../../../src/app/shared/Services/User/user.service';
import { AssessmentResult } from './../../../../../src/app/shared/Entities/User/assessment-result';
import { Component, OnInit } from '@angular/core';
import { Subscription,Observable,timer } from 'rxjs';
import { AssessmentQuestion } from 'src/app/shared/Entities/User/assessment-question';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-driver-assessment',
  templateUrl: './driver-assessment.component.html',
  styleUrls: ['./driver-assessment.component.css']
})
export class DriverAssessmentComponent implements OnInit {
  loading:boolean = false;
  isCustomComponent:boolean = false;
  isCurrentPage:boolean;
  subscription: Subscription;
  timer: Observable<any>;
  isSuccessMode = false;
  isErrorMode = true;
  isSubmitted = false;
  errorMessage: string="";
  language: string;
  journeyId:number;
  checkpointid:number;
  assessmentResult: AssessmentResult;
  formData: AssessmentResult;
  assessmentResultList: AssessmentResult[];
  preTripAssessmentQuestionList: AssessmentQuestion[];
  checkPointAssessmentQuestionList: AssessmentQuestion[];
  postTripAssessmentQuestionList: AssessmentQuestion[];

  constructor(private userService: UserService) {
    this.language = window.location.href.indexOf("/ar/") > -1 ? "ar" : "en";
  }
  ngOnInit() {
    this.isCustomComponent=false;
    this.setTimer();
    this.GetPreTripAssessment();
}
GetPreTripAssessment() {
  this.userService.GetPreTripAssessment(this.journeyId).subscribe({
    next: (result: any) => {
      let response = result as AssessmentQuestion[];
      if (!response || response.length == 0) {
        this.setErrorFlags();
        return null;
      }
      this.preTripAssessmentQuestionList = response;
      this.loading = false;
    },
    error: err => {
      this.setErrorFlags();
    }
  });
}
GetCheckPointAssessment() {
  this.userService.GetCheckPointAssessment(this.checkpointid).subscribe({
    next: (result: any) => {
      let response = result as AssessmentQuestion[];
      if (!response || response.length == 0) {
        this.setErrorFlags();
        return null;
      }
      this. checkPointAssessmentQuestionList = response;
      this.loading = false;
    },
    error: err => {
      this.setErrorFlags();
    }
  });
}
GetPostTripAssessment() {
  this.userService.GetPostTripAssessment(this.journeyId).subscribe({
    next: (result: any) => {
      let response = result as AssessmentQuestion[];
      if (!response || response.length == 0) {
        this.setErrorFlags();
        return null;
      }
      this. postTripAssessmentQuestionList = response;
      this.loading = false;
    },
    error: err => {
      this.setErrorFlags();
    }
  });
}
setErrorFlags() {
  this.loading = false;
  this.isSuccessMode = false;
  this.isErrorMode = true;
}
setLoadingAssessmentsErrorFlags() {
  this.errorMessage = "An Error Occured during loading assessment";
  this.loading = false;
  this.isSuccessMode = false;
  this.isErrorMode = true;
}
setSubmitAssessmentErrorFlags() {
  this.errorMessage = "An Error Occured during adding assessment";
  this.loading = false;
  this.isSuccessMode = false;
  this.isErrorMode = true;
}
resetForm(form?: NgForm) {
  if ((form = null)) {
    form.resetForm();
  }
  this.formData = new AssessmentResult();
  };
public setTimer(){
  this.loading   = true;
  this.timer = timer(500);
  this.subscription = this.timer.subscribe(() => {
      this.loading = false;
  });
}
onSubmit(form: NgForm) {
  this.isSubmitted = true;
  if (!form.valid) {
    return;
  }
  if ((form.value.ID == null || form.value.ID === 0)) {
    this.userService.SubmitAssessment(form.value).subscribe({
      next: result => {
        if (!result) {
          this.setSubmitAssessmentErrorFlags();
          return;
        }
        this.isSubmitted = false;
        this.resetForm(form);
        this.loading = false;
        this.isSuccessMode = true;
        this.errorMessage="";
      },
      error: err => {
        console.log("err:=> ",err);
        this.setSubmitAssessmentErrorFlags();
      }
    });
  } else{
    this.setSubmitAssessmentErrorFlags();
  }
}
}
