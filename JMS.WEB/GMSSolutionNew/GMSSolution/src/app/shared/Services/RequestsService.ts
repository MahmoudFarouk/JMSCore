import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { RequestModel } from '../Models/RequestModel';

@Injectable({ providedIn: 'root' })
export class RequestService {

    constructor(private http: HttpClient) { }

    getRequests() {
        let requestUrl: string = `${environment.JMSApiURL}/request/getrequests`;
        return this.http.get<RequestModel[]>(requestUrl);
    }
}