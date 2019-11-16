import { Component, OnInit } from '@angular/core';
import { Subscription,Observable,timer } from 'rxjs';

@Component({
  selector: 'app-journey-preparation',
  templateUrl: './journey-preparation.component.html',
  styleUrls: ['./journey-preparation.component.css']
})
export class JourneyPreparationComponent implements OnInit {
  loading:boolean = false;
  isCustomComponent:boolean = false;
  isCurrentPage:boolean;
  subscription: Subscription;
  timer: Observable<any>;
  constructor() {
    
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
