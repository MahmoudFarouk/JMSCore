import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { ServiceResponse } from '../../shared/Models/ServiceResponseModel';
import { CheckPoint } from '../../shared/models/JourneDetailModel';

@Injectable({ providedIn: 'root' })
export class InitiateJourneyService {

    constructor(private http: HttpClient) {
    }

    getCheckpoints(fromLat, fromLng, toLat, toLng) {

        let requestUrl: string = `${environment.JMSApiURL}/checkpoint/getcheckpoints?fromLat=${fromLat}&fromLng=${fromLng}&toLat=${toLat}&toLng=${toLng}`;

        return this.http.get<ServiceResponse<CheckPoint[]>>(requestUrl).pipe(retry(1), catchError(this.errorHandl));
    }


    //getAllCustomers() {
    //    return this.http.get<ServiceResponse<Customer>>(this.baseUrl + 'api/customer/getall')
    //        .pipe(retry(1), catchError(this.errorHandl));
    //}

    //updateVehicleStatus() {
    //    return this.http.post(this.baseUrl + 'api/vehicle/updatestatus', null)
    //        .pipe(retry(1), catchError(this.errorHandl));
    //}

    private errorHandl(error) {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
            errorMessage = error.error.message;
        } else {
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        console.log(errorMessage);
        return throwError(errorMessage);
    }
}
