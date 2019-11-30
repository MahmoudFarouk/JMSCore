import { Component, OnInit } from '@angular/core';
import { Subscription, Observable, timer } from 'rxjs';
import { User } from '../shared/models/UserModel';
import { AuthenticationService } from '../shared/Services/AuthenticationService';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    currentUser: User;
    data: any;

    constructor(private authenticationService: AuthenticationService) {
        this.currentUser = this.authenticationService.currentUserValue;
    }
    ngOnInit() {
       
    }
    
}
