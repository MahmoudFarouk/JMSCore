import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from './../../../environments/environment';
import { AuthenticationService } from './../../shared/Services/AuthenticationService';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add auth header with jwt if user is logged in and request is to api url
        const currentUser = this.authenticationService.currentUserValue;
        const isLoggedIn = currentUser && currentUser.data.token;
        const isApiUrl = request.url.startsWith(environment.JMSApiURL);
        debugger;
        if (isLoggedIn && isApiUrl&& request.url.indexOf('ForgetPassword') == -1) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentUser.data.token}`
                }
            });

        }
        return next.handle(request);
    }
}
