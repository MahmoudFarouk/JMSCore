import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { throwError } from 'rxjs';
import { ServiceResponse } from '../../shared/Models/ServiceResponseModel';
import { JourneyModel } from '../models/JourneyModel';
import swal from "sweetalert2";
import { Checkpoint } from '../models/CheckpointModel';
import { LookupModel } from '../Models/LookupModel';

@Injectable({ providedIn: 'root' })
export class JourneyService {

    constructor(private http: HttpClient) {
    }

    getCheckpoints(fromLat, fromLng, toLat, toLng) {

        let requestUrl: string = `${environment.JMSApiURL}/checkpoint/getcheckpoints?fromLat=${fromLat}&fromLng=${fromLng}&toLat=${toLat}&toLng=${toLng}`;

        return this.http.get<ServiceResponse<Checkpoint[]>>(requestUrl);
    }

    getDispatchers() {
        let requestUrl: string = `${environment.JMSApiURL}/user/getdispatchers`;

        return this.http.get<ServiceResponse<LookupModel[]>>(requestUrl);
    }

    initJourney(model) {
        return this.http.post<ServiceResponse<any>>(`${environment.JMSApiURL}/journey/initiate`, model);
    }

    validateJourney(model) {
        return this.http.post<ServiceResponse<any>>(`${environment.JMSApiURL}/journey/validate`, model);
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

    GetJourneySelectDriver(id) {

        return this.http.get<any>(`${environment.JMSApiURL}/journey/JourneySelectDriver?journeyId=${id}`);
    }

    UpdateJourneyStatus(journeyId, status) {

        return this.http.post<any>(`${environment.JMSApiURL}/journey/updateJourneyStatus?journeyId=${journeyId}&status=${status}`, {});
    }
    AddJourneyUpdate(data) {
        return this.http.post<any>(`${environment.JMSApiURL}/journey/addJourneyUpdate1`, data);

    }
    GetJourneyMontoring(journeyId) {
        return this.http.get<any>(`${environment.JMSApiURL}/journey/GetJourneyMontoring?journeyId=${journeyId}`);
    }


    getAllJourneyInfo(id) {
        let requestUrl: string = `${environment.JMSApiURL}/journey/getalljourneyinfo/${id}`;
        return this.http.get<JourneyModel>(requestUrl);
    }
    UpdateJourneyRiskStatus(journeyId, isNight, status) {

        isNight = isNight == null ? '' : isNight;
        status = status == null ? '' : status;
        return this.http.post<any>(`${environment.JMSApiURL}/journey/updateJourneyRiskStatus?journeyId=${journeyId}&isNight=${isNight}&status=${status}`, {});
    }
    RejectJourney(journeyId, comment) {
        return this.http.post<any>(`${environment.JMSApiURL}/journey/rejectJourney?journeyId=${journeyId}&comment=${comment}`, {});
    }
    GetCurrentJourney(){
        return this.http.get<any>(`${environment.JMSApiURL}/journey/getCurrentJourney`);
    }
    CloseJourney(journeyId, comment) {
        return this.http.post<any>(`${environment.JMSApiURL}/journey/closeJourney?journeyId=${journeyId}&comment=${comment}`, {});
    }
}
