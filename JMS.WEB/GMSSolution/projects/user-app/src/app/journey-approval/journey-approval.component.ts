import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-journey-approval',
  templateUrl: './journey-approval.component.html',
  styleUrls: ['./journey-approval.component.css']
})
export class JourneyApprovalComponent implements OnInit {

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
