import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/UserModel';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(username: string, password: string) {
        return this.http.post<any>(`${environment.JMSApiURL}/user/authenticate`, { username, password })
            .pipe(map(user => {
                // login successful if there's a jwt token in the response
                if (user.data && user.data.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user.data));
                    this.currentUserSubject.next(user.data);
                }
                return user.data;
            }));
    }
    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
      async forgetPassword(username){
        
        return  await this.http.post<any>(environment.JMSApiURL + `/user/ForgetPassword?username=${username}`,{}).toPromise();

    }
    async forgetChangePassword(token,newpassword){
        
        return  await this.http.post<any>(environment.JMSApiURL + `/user/ResetForgetPassword?token=${token}&newPassword=${newpassword}`,{}).toPromise();

    }
   //Teaams
    async GetTeams(){
        return await this.http.get(environment.JMSApiURL + `/user/getgroups`).toPromise();
    }
    async AddTeam(name){
        return await this.http.post<any>(environment.JMSApiURL + `/user/addgroup`,{Name:name}).toPromise();
    }
    async DeleteTeam(groupId){
        return await this.http.post<any>(environment.JMSApiURL + `/user/deletegroup?groupId=${groupId}`,{}).toPromise();
    }

    //Workforce
    
    async GetWorkFoces(){
        return await this.http.get(environment.JMSApiURL + `/user/getworkforces`).toPromise();
    }
    async AddWorkForce(name){
        return await this.http.post<any>(environment.JMSApiURL + `/user/addworkforce`,{Name:name}).toPromise();
    }
    async DeleteWorkForce(workforceId){
        return await this.http.post<any>(environment.JMSApiURL + `/user/deleteworkforce?workforceId=${workforceId}`,{}).toPromise();
    }

    //users
    async GetAllUsers(){
        return await this.http.get<any>(environment.JMSApiURL + `/user/getAllUsers`).toPromise();
    }
    async Register(data){
        return await this.http.post<any>(environment.JMSApiURL + `/user/register`,data).toPromise();

    }

}
