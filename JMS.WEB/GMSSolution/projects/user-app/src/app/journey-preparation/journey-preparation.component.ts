import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-journey-preparation',
  templateUrl: './journey-preparation.component.html',
  styleUrls: ['./journey-preparation.component.css']
})
export class JourneyPreparationComponent implements OnInit {

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
