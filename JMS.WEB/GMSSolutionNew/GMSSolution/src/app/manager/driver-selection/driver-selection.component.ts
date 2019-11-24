import { Component, OnInit } from '@angular/core';
import { Subscription, Observable, timer } from 'rxjs';
import { DriverModel } from 'src/app//shared/Entities/User/DriverModel';
import { DriverService } from 'src/app//shared/Services/User/DriverService';
import { JourneyService } from 'src/app/shared/Services/User/JourneyService';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-driver-selection',
  templateUrl: './driver-selection.component.html',
  styleUrls: ['./driver-selection.component.css']
})
export class DriverSelectionComponent implements OnInit {
  loading: boolean = false;
  isCustomComponent: boolean = false;
  isCurrentPage: boolean;
  subscription: Subscription;
  timer: Observable<any>;
  Drivers: DriverModel[];
  
  slectedDriver: any = {};
  JourneyId: number =0;
  journeyUpdateId:number=0;
  keyword:string="";
  constructor(private DriverService: DriverService, private JourneyService: JourneyService, private activatedRoute: ActivatedRoute) {

  }
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      const journeyId = params['journeyId'];
      this.JourneyId = journeyId != undefined && journeyId != null && journeyId != '' ? journeyId : 0;

    });
    this.isCustomComponent = false;
    //this.setTimer();
    this.loading = true;
    this.DriverService.GetDrivers().toPromise().then((data: any) => {
      
      this.Drivers = data.data;
      this.JourneyService.GetJourneySelectDriver(this.JourneyId).toPromise().then((data: any) => {
        debugger;
        this.slectedDriver.id = data.data.DriverId
        this.journeyUpdateId=data.data.Id;
        
      }, (error) => {
        this.loading = false;
      });
      this.loading = false;
    }, (error) => {
      this.loading = false;
    });

  }
   Search($event){
    this.DriverService.GetDrivers(this.keyword).toPromise().then((data: any) => {
    
      this.Drivers = data.data;
    });
  }
  requireVclNo(item: DriverModel) {
    return this.slectedDriver !== null && this.slectedDriver.id === item.id && (item.VechNo === undefined || item.VechNo === null || item.VechNo === '');
  }
  public async selectClick(item: DriverModel) {
   
    this.slectedDriver.id = item.id;
    if (item.VechNo !== undefined && item.VechNo !== '' && item.VechNo !== null) {
      this.loading = true;

      var result = await this.JourneyService.AssignDriverToJourney({
        Id:this.journeyUpdateId,
        JourneyId: this.JourneyId,
        DriverId: this.slectedDriver.id,
        VehicleNo: item.VechNo

      }) as any;
      debugger;
      if (result.status === 1) {
        
        this.journeyUpdateId=result.data;
        
        alert("Driver assigned successfully");
      } else {
        alert("Server Error");
      }
      this.loading = false;

    } else {
      alert('please enter Vehicle.No')
    }
  }
  public setTimer() {
    this.loading = true;
    this.timer = timer(500);
    this.subscription = this.timer.subscribe(() => {
      this.loading = false;
    });
  }

}
