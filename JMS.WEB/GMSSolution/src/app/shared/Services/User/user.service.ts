import { AssessmentResult } from './../../Entities/User/assessment-result';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from './../../../../environments/environment.prod';
@Injectable({
  providedIn: 'root'
})
export class UserService {
  Headers:HttpHeaders;
  constructor(private Http: HttpClient) {
    this.Headers=new HttpHeaders();
    this.Headers.set("Content-Type","application/json");
  }
  GetPreTripAssessment(journeyId: number) {
    return this.Http.get(environment.JMSApiURL + '/driver/getpretripassessment/'+ journeyId);
  }
  GetPostTripAssessment(journeyId: number) {
    return this.Http.get(environment.JMSApiURL + '/driver/getposttripassessment/'+ journeyId);
  }
  GetCheckPointAssessment(checkpointid: number) {
    return this.Http.get(environment.JMSApiURL + '/driver/getcheckpointassessment/'+ checkpointid);
  }
  SubmitAssessment(assessmentResult: AssessmentResult) {
    return this.Http.post(environment.JMSApiURL + '/submitassessment', assessmentResult);
  }

}
