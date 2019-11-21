import { Injectable } from '@angular/core';

import { Driver } from '../Models/Driver';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../Constants';
@Injectable({ providedIn: 'root' })
export class DriverService{
    headers = {
        "Content-Type": "application/json",
        "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImJkNGQ1NGUyLTk2NzgtNGQwNi05YzVhLTA4ZDc2N2MzMTAyNCIsInJvbGUiOlsiUEwiLCJEaXNwYXRjaGVyIiwiRHJpdmVyIl0sIm5iZiI6MTU3NDExMjYyNywiZXhwIjoxNTc0NzE3NDI3LCJpYXQiOjE1NzQxMTI2Mjd9.f-kMqt--kiy9beHNk5wDov7ZLxfgedWcfxpwoa8wN8I"
    }
    constructor(private http: HttpClient) { 
       
    }
     GetDrivers(drivername=""){

        return  this.http.get(`${Constants.Url}/api/driver/getdrivers?drivername=${drivername}`,{headers:this.headers});
        
    }

}