import { Component, OnInit } from '@angular/core';
import { Subscription,Observable,timer } from 'rxjs';
@Component({
  selector: 'app-my-requests',
  templateUrl: './my-requests.component.html',
  styleUrls: ['./my-requests.component.css']
})
export class MyRequestsComponent implements OnInit {
  loading:boolean = false;
  isCustomComponent:boolean = false;
  isCurrentPage:boolean;
  subscription: Subscription;
  timer: Observable<any>;
  isSuccessMode = false;
  isErrorMode = true;
  isSubmitted = false;
  errorMessage: string="";
  language: string;
  constructor() {
    this.language = window.location.href.indexOf("/ar/") > -1 ? "ar" : "en";
  }
  ngOnInit() {
    this.isCustomComponent=false;
    this.setTimer();
}
public setTimer(){
  this.loading   = true;
  this.timer = timer(500);
  this.subscription = this.timer.subscribe(() => {
      this.loading = false;
  });
}
}
