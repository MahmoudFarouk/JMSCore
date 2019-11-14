import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-journey-closure',
  templateUrl: './journey-closure.component.html',
  styleUrls: ['./journey-closure.component.css']
})
export class JourneyClosureComponent implements OnInit {

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
