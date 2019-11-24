import { Injectable } from '@angular/core';


import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JourneyModel } from '../../Entities/User/JourneyModel';
import { environment } from '../../../../environments/environment';
@Injectable({ providedIn: 'root' })
export class JourneyService {
    
    constructor(private http: HttpClient) {

    }
    GetJourneyInfo(id) {

        return this.http.get<JourneyModel>(`${environment.JMSApiURL}/journey/journeyInfo?id=${id}`);

    }
    GetJourneyDetails(id) {

        return this.http.get<JourneyModel>(`${environment.JMSApiURL}/journey/${id}`);

    }
    async AssignDriverToJourney(model) {

        return await this.http.post(`${environment.JMSApiURL}/journey/AssignDriverToJourney`,model).toPromise();

    }
    GetJourneySelectDriver(id) {

        return this.http.get<any>(`${environment.JMSApiURL}/journey/JourneySelectDriver?journeyId=${id}`);

    }


}