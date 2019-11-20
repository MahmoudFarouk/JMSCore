import { Component } from '@angular/core';
import { Subscription,Observable,timer } from 'rxjs';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  loading:boolean = false;
  isCustomComponent:boolean = false;
  isCurrentPage:boolean = false;
  subscription: Subscription;
  timer: Observable<any>;
  constructor() {
    
  }
  ngOnInit() {
    this.isCustomComponent = false;
    if(window.location.href.indexOf('/InitiateJourney') > -1
      || window.location.href.indexOf('/DriverSelection') > -1
      || window.location.href.indexOf('/JourneyStartingAndMonitoring') > -1
      || window.location.href.indexOf('/JourneyApproval') > -1|| window.location.href.indexOf('/Notifications') > -1){
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
