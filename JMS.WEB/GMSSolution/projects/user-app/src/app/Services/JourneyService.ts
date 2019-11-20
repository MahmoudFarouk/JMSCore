import { Injectable } from '@angular/core';

import { Driver } from '../Models/Driver';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../Constants';
import { JourneyModel } from '../Models/JourneyModel';
@Injectable({ providedIn: 'root' })
export class JourneyService {
    Headers: HttpHeaders;
    headers = {
        "Content-Type": "application/json",
        "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImJkNGQ1NGUyLTk2NzgtNGQwNi05YzVhLTA4ZDc2N2MzMTAyNCIsInJvbGUiOlsiUEwiLCJEaXNwYXRjaGVyIiwiRHJpdmVyIl0sIm5iZiI6MTU3NDEyNDI3NCwiZXhwIjoxNTc0NzI5MDc0LCJpYXQiOjE1NzQxMjQyNzR9.V17Ra0FMYc7FtocbyreyYmNsu_Gb8r1t08ryP0hG8sQ"
    }
    constructor(private http: HttpClient) {

    }
    GetJourneyInfo(id) {

        return this.http.get<JourneyModel>(`${Constants.Url}/api/journey/journeyInfo?id=${id}`, { headers: this.headers });

    }
    async AssignDriverToJourney(model) {

        return await this.http.post(`${Constants.Url}/api/journey/AssignDriverToJourney`,model, { headers: this.headers }).toPromise();

    }
    GetJourneySelectDriver(id) {

        return this.http.get<any>(`${Constants.Url}/api/journey/JourneySelectDriver?journeyId=${id}`, { headers: this.headers });

    }


}