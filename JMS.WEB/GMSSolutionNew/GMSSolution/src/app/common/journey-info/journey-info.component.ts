import { Component, OnInit, ChangeDetectorRef, Input } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { JourneyService } from './../../shared/Services/JourneyService';
import { JourneyModel } from '../../shared/models/JourneyModel';
import { JourneyUpdate } from '../../shared/models/JourneyUpdateModel';
import { JourneyStatus } from '../../shared/enums/journey-status.enum';
import { General } from '../../shared/Helpers/General';

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
        title: '',
        toDestination: null,
        toLat: null,
        toLng: null,
        userId: "",
        journeyUpdates: null,
        checkpoints: null,
        assessmentQuestion: null,
        userFullname: ""
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
        
            this.loading = false;

        }, (error) => { this.loading = false });
    }
    getStatusName(status: JourneyStatus) {
        return  General.GetStatusName(status)
        }

}
