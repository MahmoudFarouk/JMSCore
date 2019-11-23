import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment.prod';

@Injectable({ providedIn: 'root' })
export class NotificationService{
    
    constructor(private http: HttpClient) { 
       
    }
     GetNotifications(){

        return  this.http.get(`${environment.JMSApiURL}/notification/getusernotifications`);
        
    }

}