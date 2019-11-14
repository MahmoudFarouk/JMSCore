import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-initiate-journey',
  templateUrl: './initiate-journey.component.html',
  styleUrls: ['./initiate-journey.component.css']
})
export class InitiateJourneyComponent implements OnInit {

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
