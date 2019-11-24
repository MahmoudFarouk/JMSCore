import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/shared/Entities/Login/user';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/shared/Services/Login/authentication.service';
import { CommonService } from "src/app/shared/Services/CommonService";


@Component({
    selector: 'app-main-header',
    templateUrl: './main-header.component.html',
    styleUrls: ['./main-header.component.css']
})
export class MainHeaderComponent implements OnInit {

    currentUser: User;
    componentName: string;

    constructor(private router: Router, private authenticationService: AuthenticationService, private common: CommonService) {
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
        this.common.currentComponent.subscribe(name => this.componentName = name)
    }

    ngOnInit() {

    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }

}
