import { Component, OnInit } from '@angular/core';

import { Subscription,Observable,timer } from 'rxjs';
import { NotificationModel } from '../shared/Entities/User/NotificationModel';
import { NotificationService } from '../shared/Services/User/NotificationService';
@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {
  loading: boolean = false;
  timer: Observable<any>;
  Notifications: NotificationModel[];
  subscription: Subscription;
  constructor(private NotificationService:NotificationService) { }

  ngOnInit() {
    
    this.loading = true;
    
    this.NotificationService.GetNotifications().toPromise().then((data: any) => {
     
      console.log(data);
      this.Notifications = data.data;
      this.setTimer();
      this.loading = false;
    }, (error) => { 
      this.loading = false;
    });
    
  }
  public setTimer(){
    this.timer = timer(3000);
    this.subscription = this.timer.subscribe(() => {
      this.NotificationService.GetNotifications().toPromise().then((data: any) => {
        this.Notifications = data.data;
       
        this.setTimer();
       
      }, (error) => { 
       
      });
    });
  }
}
