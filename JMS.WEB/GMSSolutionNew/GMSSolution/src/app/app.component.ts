import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './shared/Services/AuthenticationService';
import { User } from './shared/models/UserModel';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

    @BlockUI() blockUI: NgBlockUI;

    title = 'GMS';
    currentUser: User;
    componenetName: string;

    constructor(private router: Router, private authenticationService: AuthenticationService) {        
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    }

    getComponentName($event) {
        this.componenetName = $event
        debugger;
    }
    ngOnInit(): void {

    }
}
