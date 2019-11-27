import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class DriverService{
   
    constructor(private http: HttpClient) { 
       
    }
     GetDrivers(drivername=""){
         debugger;
        return  this.http.get(`${environment.JMSApiURL}/driver/getdrivers?drivername=${drivername}`);
        
    }
    GetPretripAssessment(journeyId){
        return  this.http.get(`${environment.JMSApiURL}/driver/getpretripassessment?journeyid=${journeyId}`);
        
    }
    GetPostTripAssessment(journeyId){
        return  this.http.get(`${environment.JMSApiURL}/driver/getposttripassessment?journeyid=${journeyId}`);
        
    }
    GetCheckpointAssessment(checkPointId){
        return  this.http.get(`${environment.JMSApiURL}/driver/getcheckpointassessment?checkpointid=${checkPointId}`);
        
    }
    
    SubmitAssessments(journeyId,status,data){
        return  this.http.post(`${environment.JMSApiURL}/driver/submitassessment?journeyId=${journeyId}&status=${status}`,data);

    }

}
