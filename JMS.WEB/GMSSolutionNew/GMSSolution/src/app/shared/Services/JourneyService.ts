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
<<<<<<< HEAD

    UpdateJourneyStatus(journeyId,status) {

        return this.http.post<any>(`${environment.JMSApiURL}/journey/updateJourneyStatus?journeyId=${journeyId}&status=${status}`,{});
    }

    private errorHandl(error) {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
            errorMessage = error.error.message;
        } else {
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }

        swal.fire("Error", "Sorry an error occured, Please try again or contact administration.", "error");
        console.log(errorMessage);
        return throwError(errorMessage);
    }
=======
>>>>>>> b79b7c92752be7bacd4d5d96c2562d7892011fe8
}
