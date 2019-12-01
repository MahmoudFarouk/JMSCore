import { Component, OnInit } from '@angular/core';
import { Subscription,Observable,timer } from 'rxjs';
import { JourneyService } from '../../shared/Services/JourneyService';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-journey-montoring',
  templateUrl: './journey-montoring.component.html',
  styleUrls: ['./journey-montoring.component.css']
})
export class JourneyMontroing implements OnInit {
  journeyId: number;
  journeyMontroings:any[]=[];
  constructor(private JourneyService:JourneyService,private activatedRoute: ActivatedRoute) {
    
  }
  ngOnInit() { 
    this.activatedRoute.queryParams.subscribe(params => {
      const journeyId = params['journeyId'];
      
      this.journeyId = journeyId != undefined && journeyId != null && journeyId != '' ? journeyId : 0;
     
  });
    this.JourneyService.GetJourneyMontoring(this.journeyId).toPromise().then((data: any) => {
      this.journeyMontroings=data.data;
      console.log( this.journeyMontroings);
     debugger
    }, (error) => { 
      
    });
 }


}
