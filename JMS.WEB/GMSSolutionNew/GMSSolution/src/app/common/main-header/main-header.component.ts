import { Component, OnInit,AfterViewInit  } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../shared/Services/AuthenticationService';
import { User } from '../../shared/models/UserModel';
import { Subscription, Observable, timer } from 'rxjs';
import { NotificationModel } from '../../shared/models/NotificationModel';
import { NotificationService } from '../../shared/Services/NotificationService';
import * as M from 'materialize-css'
declare var $:any;
@Component({
  selector: 'app-main-header',
  templateUrl: './main-header.component.html',
  styleUrls: ['./main-header.component.css']
})
export class MainHeaderComponent implements OnInit, AfterViewInit  {
  ngAfterViewInit(): void {
    setTimeout( function() {
      var elem = document.querySelector('.sidenav');
      var instance = M.Sidenav.init(elem, {});
    }, 0)
  }
 

  currentUser: User;
  componentName: string;
  loading: boolean = false;
  timer: Observable<any>;
  Notifications: NotificationModel[] = [];
  subscription: Subscription;
  sideNavElement = "slideout";

  constructor(private NotificationService: NotificationService, private router: Router, private authenticationService: AuthenticationService) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }


  ngOnInit() {

    

    this.loading = true;

    if (this.currentUser)
      this.NotificationService.GetNotifications().toPromise().then((data: any) => {

        this.Notifications = data.data;
        //this.setTimer();
        this.loading = false;
      }, (error) => {
        this.loading = false;
      });

  }
  public setTimer() {
    this.timer = timer(3000);
    this.subscription = this.timer.subscribe(() => {
      this.NotificationService.GetNotifications().toPromise().then((data: any) => {
        this.Notifications = data.data;

        this.setTimer();

      }, (error) => {

      });
    });
  }
  navigateToNotifications() {
    this.router.navigate(['/notifications']);
  }

  // toggleSideMenu(){
  //   document.getElementById("slide-out").setAttribute("style","transform: translateX(0%);");
  // }

  logout() {
    this.authenticationService.logout(); 
  //  document.getElementById('slideout').style.transform="translateX(-105%)";
  //  document.body.style.overflow=null;
  // $(".sidenav-overlay").hide();
  $(".sidenav-overlay").click(); 
    this.router.navigate(['/login']);
  }

}
