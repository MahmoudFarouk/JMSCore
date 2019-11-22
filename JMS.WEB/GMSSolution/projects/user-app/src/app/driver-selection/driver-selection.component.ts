import { Component, OnInit } from '@angular/core';
import { Subscription,Observable,timer } from 'rxjs';
import { DriverService } from '../Services/DriverService';
import { Driver } from '../Models/Driver';

@Component({
  selector: 'app-driver-selection',
  templateUrl: './driver-selection.component.html',
  styleUrls: ['./driver-selection.component.css']
})
export class DriverSelectionComponent implements OnInit {
  loading:boolean = false;
  isCustomComponent:boolean = false;
  isCurrentPage:boolean;
  subscription: Subscription;
  timer: Observable<any>;
  Drivers:Driver[];
  constructor(private DriverService:DriverService) {
    
  }
   ngOnInit() {
    this.isCustomComponent=false;
    this.setTimer();
    this.loading=true;
     this.DriverService.GetDrivers().toPromise().then((data)=>{
       console.log(data);
       this.Drivers=data;
       this.loading=false;
     },(error)=>{
      this.loading=false;       
     });
    
 }
public setTimer(){
  this.loading   = true;
  this.timer = timer(500);
  this.subscription = this.timer.subscribe(() => {
      this.loading = false;
  });
}

}
