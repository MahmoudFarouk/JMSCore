import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class NotificationService{
    
    constructor(private http: HttpClient) { 
       
    }
     GetNotifications(){

        return  this.http.get(`${environment.JMSApiURL}/notification/getusernotifications`);
        
    }
    AddNotification(notification){
        return  this.http.post(`${environment.JMSApiURL}/notification/addnotification`,notification);
    }

}
