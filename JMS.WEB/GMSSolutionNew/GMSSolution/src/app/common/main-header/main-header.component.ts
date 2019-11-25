import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../shared/Services/AuthenticationService';
import { User } from '../../shared/models/UserModel';

@Component({
    selector: 'app-main-header',
    templateUrl: './main-header.component.html',
    styleUrls: ['./main-header.component.css']
})
export class MainHeaderComponent implements OnInit {

    currentUser: User;
    componentName: string;

    constructor(private router: Router, private authenticationService: AuthenticationService) {
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    }

    ngOnInit() {

    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }

}
