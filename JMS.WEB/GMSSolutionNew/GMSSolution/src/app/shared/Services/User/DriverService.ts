import { Injectable } from '@angular/core';
import { Driver } from './../../Entities/User/Driver';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Injectable({ providedIn: 'root' })
export class DriverService{
    Headers:HttpHeaders;
    constructor(private http: HttpClient) { 
        this.Headers=new HttpHeaders();
        this.Headers.set("Content-Type","application/json");
        this.Headers.set("Authorization","Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImJkNGQ1NGUyLTk2NzgtNGQwNi05YzVhLTA4ZDc2N2MzMTAyNCIsInJvbGUiOlsiUEwiLCJEaXNwYXRjaGVyIl0sIm5iZiI6MTU3MzgyNTUyOCwiZXhwIjoxNTc0NDMwMzI4LCJpYXQiOjE1NzM4MjU1Mjh9.Utv9CJdcTtI0NmHix1mLthdepHcyYO8nGtkXM1jZrzs");
        
    }
     GetDrivers(){

        return  this.http.get<Driver[]>(`${environment.JMSApiURL}/driver/getdrivers`,{headers:this.Headers});
        
    }

}