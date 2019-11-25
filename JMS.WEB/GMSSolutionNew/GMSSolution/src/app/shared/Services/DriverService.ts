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

}
