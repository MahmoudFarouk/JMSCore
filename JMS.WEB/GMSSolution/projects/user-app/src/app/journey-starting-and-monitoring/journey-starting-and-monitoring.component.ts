import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-journey-starting-and-monitoring',
  templateUrl: './journey-starting-and-monitoring.component.html',
  styleUrls: ['./journey-starting-and-monitoring.component.css']
})
export class JourneyStartingAndMonitoringComponent implements OnInit {

loading = false;
isSuccessMode = false;
isErrorMode = true;
language : string;

  constructor() {
    this.language = window.location.href.indexOf('/ar/') > -1 ? 'ar' : 'en';
  }
  ngOnInit() {
    this.loading = true;
 }
setErrorFlags() {
  this.loading = false;
  this.isSuccessMode = false;
  this.isErrorMode = true;
}

}
