import { Component, OnInit } from '@angular/core';
import { Subscription, Observable, timer } from 'rxjs';
import { User } from '../shared/models/UserModel';
import { AuthenticationService } from '../shared/Services/AuthenticationService';
import { JourneyService } from '../shared/Services/JourneyService';
import Swal from 'sweetalert2';
import { JourneyStatus } from '../shared/enums/journey-status.enum';
import { Router } from '@angular/router';
@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    currentUser: User;
    data: any;
    currentJourney: any = null;
    constructor(private authenticationService: AuthenticationService, private journeyService: JourneyService, private router: Router) {
        this.currentUser = this.authenticationService.currentUserValue;
    }
    ngOnInit() {
        var userRole = this.currentUser.roles[0].name;
        if (userRole == 'Driver') {
            this.journeyService.GetCurrentJourney().toPromise().then((data) => {
                
                this.currentJourney = data.data;
                console.log(this.currentJourney);
            });
        }

    }
    CurrentJourny() {
        if (this.currentJourney == null) {
            Swal.fire('', 'There is no pending trip', 'info');

            return;
        }

        switch (this.currentJourney.journeyStatus) {
            case JourneyStatus.PendingOnDriverCompletePreTripAssessment:
            case JourneyStatus.PendingOnDriverCompletePostTripAssessment:
                this.router.navigate([`/driver/assessment/`], { queryParams: { journeyId: this.currentJourney.id } });
                break;
            case JourneyStatus.PendingOnDriverCompleteCheckpointAssessment:
            case JourneyStatus.PendingOnDriverStartJourney:
                this.router.navigate([`/driver/journey/`], { queryParams: { journeyId: this.currentJourney.id } });
                break;
            default:
                this.router.navigate([`/journeyapproval`], { queryParams: { journeyId: this.currentJourney.id } });

                break;

        }
    }

}
