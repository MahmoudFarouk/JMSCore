import { Component, OnInit } from '@angular/core';
import { Subscription,Observable,timer } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loading:boolean = false;
  isCustomComponent:boolean = false;
  isCurrentPage:boolean = false;
  subscription: Subscription;
  timer: Observable<any>;

  constructor() {
    
  }
  ngOnInit() {
    this.setTimer();
 }
 public setTimer(){
  this.loading   = true;
  this.timer = timer(3000);
  this.subscription = this.timer.subscribe(() => {
      this.loading = false;
  });
}

}
