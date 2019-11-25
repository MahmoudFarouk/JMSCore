import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CheckPoint } from '../models/JourneDetailModel';
import { environment } from '../../../environments/environment';
import { JourneyModel } from '../models/JourneyModel';
import { ServiceResponse } from '../models/ServiceResponseModel';

@Injectable({ providedIn: 'root' })

export class JourneyService {

    constructor(private http: HttpClient) {

    }

    getCheckpoints(fromLat, fromLng, toLat, toLng) {
        let x = this.http.get<ServiceResponse<CheckPoint[]>>(`${environment.JMSApiURL}/checkpoint/getcheckpoints?fromLat=${fromLat}&fromLng=${fromLng}&toLat=${toLat}&toLng=${toLng}`);
        debugger;
        return x;
    }

    GetJourneyInfo(id) {
        return this.http.get<JourneyModel>(`${environment.JMSApiURL}/journey/journeyInfo?id=${id}`);
    }
    GetJourneyDetails(id) {
        return this.http.get<JourneyModel>(`${environment.JMSApiURL}/journey/${id}`);
    }
    async AssignDriverToJourney(model) {
        return await this.http.post(`${environment.JMSApiURL}/journey/AssignDriverToJourney`, model).toPromise();
    }
    async initJourney(model) {

        return await this.http.post(`${environment.JMSApiURL}/journey/initiate`, model).toPromise();
    }
    GetJourneySelectDriver(id) {

        return this.http.get<any>(`${environment.JMSApiURL}/journey/JourneySelectDriver?journeyId=${id}`);
    }


}
