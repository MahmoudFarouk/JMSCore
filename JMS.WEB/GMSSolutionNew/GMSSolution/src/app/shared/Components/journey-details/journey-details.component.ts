import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { AssessmentQuestion } from 'src/app/shared/Entities/User/assessment-question';
import { JourneyService } from '../../Services/User/JourneyService';
import { Data, JourneyDetailModel } from 'src/app/shared/Entities/User/JourneDetailModel';
@Component({
  selector: 'app-journey-details',
  templateUrl: './journey-details.component.html',
  styleUrls: ['./journey-details.component.css']
})
export class JourneyDetailsComponent implements OnInit {
  private JourneyId: number = 0;
  constructor(private JourneyService: JourneyService, private activatedRoute: ActivatedRoute) { }
  loading: boolean = false;
  assesments: any[];
  data: Data = {
    "id": 0,
    "title": null,
    "isTruckTransport": false,
    "journeyStatus": 0,
    "fromDestination": null,
    "fromLat": 0.0,
    "fromLng": 0.0,
    "toDistination": null,
    "toLat": null,
    "toLng": null,
    "startDate": null,
    "deliveryDate": null,
    "cargoWeight": null,
    "cargoPriority": 0,
    "cargoSeverity": 0,
    "cargoType": null,
    "userId": "00000000-0000-0000-0000-000000000000",
    "userFullname": null,
    "isThirdParty": false,
    assesments: []
  };
  assessments: any[] = [];
  journey: JourneyDetailModel = {
    data: this.data,
    status: 1,
    exception: null
  }
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      const journeyId = params['journeyId'];
      this.JourneyId = journeyId != undefined && journeyId != null && journeyId != '' ? journeyId : 0;

    });
    this.loading = true;

    this.JourneyService.GetJourneyDetails(this.JourneyId).toPromise().then((data: any) => {
      var d = "";
      d.toString()
      if (data.data.status = 1) {
        this.journey = data;
       
        /*this.assesments=data.data.assesments.length>0?data.data.assesments[0]:[]*/
        //console.log(this.assesments);
      }
      this.loading = false;
    }, (error) => {
      this.loading = false;
    });

  }
  getGourneyStatus(num) {
    var tex = "";
    switch (num) {
      case 0:
        tex = "Pending On Approval";
        break;
      case 1:
        tex = "Approved";
        break;
      case 2:
        tex = "Canceled";
        break;
      case 3:
        tex = "Stopped";
        break;
      case 4:
        tex = "Completed";
        break;
    }
    return tex;
  }
  getLevelStatus(num) {
    
    var tex = "";
    switch (num) {
      case 0:
        tex = "Low";
        break;
      case 1:
        tex = "Medium";
        break;
      case 2:
        tex = "High";
        break;
     
    }
    return tex;
  }
  getQuestionCategory(num) {
  
    var tex = "";
    switch (num) {
      case 0:
        tex = "Checkpoint Assessment";
        break;
      case 1:
        tex = "Vehicle Checklist";
        break;
      case 2:
        tex = "PreTrip";
        break;
      case 3:
        tex = "PostTrip";
        break;
    }
    return tex;
  }

}
