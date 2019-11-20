import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../Constants';
import { NotificationModel } from '../Models/NotificationModel';
@Injectable({ providedIn: 'root' })
export class NotificationService{
    headers = {
        "Content-Type": "application/json",
        "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImJkNGQ1NGUyLTk2NzgtNGQwNi05YzVhLTA4ZDc2N2MzMTAyNCIsInJvbGUiOlsiUEwiLCJEaXNwYXRjaGVyIiwiRHJpdmVyIl0sIm5iZiI6MTU3NDExMjYyNywiZXhwIjoxNTc0NzE3NDI3LCJpYXQiOjE1NzQxMTI2Mjd9.f-kMqt--kiy9beHNk5wDov7ZLxfgedWcfxpwoa8wN8I"
    }
    constructor(private http: HttpClient) { 
       
    }
     GetNotifications(){

        return  this.http.get(`${Constants.Url}/api/notification/getusernotifications`,{headers:this.headers});
        
    }

}