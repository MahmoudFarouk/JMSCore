import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { JourneyService } from 'src/app/shared/Services/User/JourneyService';
import { JourneyModel } from 'src/app/shared/Entities/User/JourneyModel';
import { Router, ActivatedRoute, Params } from '@angular/router';
@Component({
  selector: 'app-journey-info',
  templateUrl: './journey-info.component.html',
  styleUrls: ['./journey-info.component.css']
})
export class JourneyInfoComponent implements OnInit {
  loading: boolean = false;
  public JourneyId: number = 0;
  public Journey: JourneyModel = {
    cargoPriority: null,
    cargoSeverity: null,
    cargoType: null,
    cargoWeight: null,
    deliveryDate: null,
    fromDestination: null,
    fromLat: null,
    fromLng: null,
    id: null,
    isThirdParty: false,
    isTruckTransport: false,
    journeyStatus: null,
    startDate: null,
    title: null,
    toDistination: null,
    toLat: null,
    toLng: null,
    userId: ""
  };
  constructor(private JourneyService: JourneyService, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      const journeyId = params['journeyId'];
      this.JourneyId = journeyId != undefined && journeyId != null && journeyId != '' ? journeyId : 0;

    });
    this.loading = true
    this.JourneyService.GetJourneyInfo(this.JourneyId).toPromise().then((data: any) => {
      this.Journey = data.data;
      console.log(this.Journey);
      this.loading = false;

    }, (error) => { this.loading = false });
  }

}
