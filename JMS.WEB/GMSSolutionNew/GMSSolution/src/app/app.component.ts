import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './shared/Services/AuthenticationService';
import { User } from './shared/models/UserModel';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { JourneyService } from './shared/Services/JourneyService';
import { Subscription, Observable, timer } from 'rxjs';
import swal from "sweetalert2";
import { environment } from 'src/environments/environment';
declare var $:any;
@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

    @BlockUI() blockUI: NgBlockUI;

    title = 'JMS';
    currentUser: User;
    componenetName: string;
    currentJourney: any;
    timer: Observable<any>;
    constructor(private router: Router, private authenticationService: AuthenticationService, private journeyService: JourneyService) {
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    }

    getComponentName($event) {
        this.componenetName = $event


    }
    ngOnInit(): void {

        var userRole = this.currentUser.roles[0].name;
        if (userRole == 'Driver') {
            this.startTimer();
        }
    }
   
  interval;
    startTimer() {
        this.interval = setInterval(() => {
            this.journeyService.GetCurrentJourney().toPromise().then((data) => {
                this.currentJourney = data.data;
                if (this.currentJourney.journeyStatus > 8) {
                    var currentDateStr = localStorage.getItem('lastCheckin');
                    if (currentDateStr != undefined && currentDateStr != null && currentDateStr != '') {
                        var lastcheckinDate = new Date(currentDateStr);
                        var today = new Date();
                        const diffInMs =today.getTime() - lastcheckinDate.getTime();
                        debugger;
                        const diffInHours = diffInMs / 1000 / 60 / 60;
                        debugger;
                        switch (this.currentJourney.riskStatus) {
                            case 0:
                                if (diffInHours >= 2) {
                                    swal.fire("", "Please check your location now", "success").then(() => {
                                        this.router.navigate(['/driver/journey'], { queryParams: { journeyId: this.currentJourney.id } })
    
                                    });
                                }
                                break;
                            default:
                                if (diffInHours >= 1) {
                                    swal.fire("", "Please check your location now", "success").then(() => {
                                        this.router.navigate(['/driver/journey'], { queryParams: { journeyId: this.currentJourney.id } })
    
                                    });
                                }
                                break
                        }
                    } else {
                        var currentDate = new Date();
                        localStorage.setItem('lastCheckin', currentDate.toUTCString());
                    }
                }
            }, (error) => {
    
            });
        },1800000)
      }
  
}
