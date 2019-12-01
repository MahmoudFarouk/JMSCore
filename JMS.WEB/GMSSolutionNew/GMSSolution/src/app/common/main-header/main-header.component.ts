import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../shared/Services/AuthenticationService';
import { User } from '../../shared/models/UserModel';
import { Subscription, Observable, timer } from 'rxjs';
import { NotificationModel } from '../../shared/models/NotificationModel';
import { NotificationService } from '../../shared/Services/NotificationService';

@Component({
    selector: 'app-main-header',
    templateUrl: './main-header.component.html',
    styleUrls: ['./main-header.component.css']
})
export class MainHeaderComponent implements OnInit {

    currentUser: User;
    componentName: string;
    loading: boolean = false;
    timer: Observable<any>;
    Notifications: NotificationModel[]=[];
    subscription: Subscription;

    constructor(private NotificationService:NotificationService,private router: Router, private authenticationService: AuthenticationService) {
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    }
    
  
    ngOnInit() {
      
      this.loading = true;
      
      this.NotificationService.GetNotifications().toPromise().then((data: any) => {
       
        this.Notifications = data.data;
        //this.setTimer();
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
    navigateToNotifications(){
        this.router.navigate(['/notifications']);
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }

}
