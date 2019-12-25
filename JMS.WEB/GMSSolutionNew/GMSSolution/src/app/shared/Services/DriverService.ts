import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class DriverService{
   
    constructor(private http: HttpClient) { 
       
    }
     GetDrivers(drivername=""){
         
        return  this.http.get(`${environment.JMSApiURL}/driver/getdrivers?drivername=${drivername}`);
        
    }
    GetPretripAssessment(journeyId){
        return  this.http.get(`${environment.JMSApiURL}/driver/getpretripassessment?journeyid=${journeyId}`);
        
    }
    GetPostTripAssessment(journeyId){
        return  this.http.get(`${environment.JMSApiURL}/driver/getposttripassessment?journeyid=${journeyId}`);
        
    }
    GetCheckpointAssessment(checkPointId,journeyId){
        return  this.http.get(`${environment.JMSApiURL}/driver/getcheckpointassessment?checkpointid=${checkPointId}&journeyId=${journeyId}`);
        
    }
    
    SubmitAssessments(journeyId,status,data,journeyUpdateId=0){
        return  this.http.post(`${environment.JMSApiURL}/driver/submitassessment?journeyId=${journeyId}&ju=${journeyUpdateId}&status=${status}`,data);

    }
    GetCheckPointsByJourneyId(journeyId){
        return  this.http.get(`${environment.JMSApiURL}/checkpoint/getCheckpointsByJourneyId?journeyid=${journeyId}`);
        
    }

}
