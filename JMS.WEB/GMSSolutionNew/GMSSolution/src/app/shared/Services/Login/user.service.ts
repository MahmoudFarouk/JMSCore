import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../../../environments/environment';
import { User } from './../../Entities/Login/user';

@Injectable({
    providedIn: 'root'
  })
export class UserService {
    constructor(private http: HttpClient) { }
test(){
    return this.http.get<any>(`${environment.JMSApiURL}/user/test`);
}
    getAll() {
        return this.http.get<User[]>(`${environment.JMSApiURL}/user/users`);
    }

    getById(id: number) {
        return this.http.get<User>(`${environment.JMSApiURL}/user/users/${id}`);
    }
}