import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-workforce-management',
  templateUrl: './workforce-management.component.html',
  styleUrls: ['./workforce-management.component.css']
})
export class WorkforceManagementComponent implements OnInit {

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
