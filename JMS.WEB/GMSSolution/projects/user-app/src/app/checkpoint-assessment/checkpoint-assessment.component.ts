import { Component, OnInit } from '@angular/core';
import { Subscription,Observable,timer } from 'rxjs';

@Component({
  selector: 'app-checkpoint-assessment',
  templateUrl: './checkpoint-assessment.component.html',
  styleUrls: ['./checkpoint-assessment.component.css']
})
export class CheckpointAssessmentComponent implements OnInit {
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
