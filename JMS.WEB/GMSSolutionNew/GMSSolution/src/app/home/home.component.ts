import { Component, OnInit } from '@angular/core';
import { Subscription,Observable,timer } from 'rxjs';

import { UserService } from '../shared/Services/Login/user.service';
import { AuthenticationService } from '../shared/Services/Login/authentication.service';
import { User } from '../shared/Entities/Login/user';
 
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  ShowLoginThings = false;
  loading:boolean = false;
  isCustomComponent:boolean = false;
  isCurrentPage:boolean = false;
  subscription: Subscription;
  timer: Observable<any>;
  currentUser: User;
  data: any;
  constructor(private userService: UserService,
    private authenticationService: AuthenticationService) {
      this.currentUser =this.authenticationService.currentUserValue;
      console.log(this.currentUser);
  }
  ngOnInit() {
    this.isCustomComponent = false;
    if(window.location.href.indexOf('/InitiateJourney') > -1
      || window.location.href.indexOf('/DriverSelection') > -1
      || window.location.href.indexOf('/JourneyStartingAndMonitoring') > -1
      || window.location.href.indexOf('/JourneyApproval') > -1
      || window.location.href.indexOf('/DriverAssessment') > -1){
        this.isCurrentPage = true;
        this.isCustomComponent = true;
      }
      this.setTimer();
 }
navigateToCustomPage(event) {
  this.isCustomComponent = true;
  this.isCurrentPage = true;
}
public setTimer(){
  this.loading   = true;
  this.timer = timer(3000);
  this.subscription = this.timer.subscribe(() => {
      this.loading = false;
  });
}
}