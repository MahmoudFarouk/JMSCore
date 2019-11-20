import { Component, OnInit } from '@angular/core';
import { NotificationModel } from '../Models/NotificationModel';
import { NotificationService } from '../Services/NotificationService';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {
  loading: boolean = false;
  
  Notifications: NotificationModel[];
  constructor(private NotificationService:NotificationService) { }

  ngOnInit() {
    
    this.loading = true;
    
    this.NotificationService.GetNotifications().toPromise().then((data: any) => {
     
      console.log(data);
      this.Notifications = data.data;
      
      this.loading = false;
    }, (error) => {
      this.loading = false;
    });

  }
}
