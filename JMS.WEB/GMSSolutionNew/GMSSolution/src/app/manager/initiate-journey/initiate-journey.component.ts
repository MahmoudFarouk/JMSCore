import { NgZone, OnInit, Component, ChangeDetectionStrategy, ChangeDetectorRef, AfterViewInit, ViewEncapsulation, ViewChild, ElementRef } from '@angular/core';
import { MapsAPILoader } from '@agm/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { timer, Observable, Subscription } from 'rxjs';
import { InitiateJourneyService } from '../initiate-journey/journey-service'
import { HttpClient } from '@angular/common/http';
import { CheckPoint } from '../../shared/models/JourneDetailModel';
import { JourneyModel } from '../../shared/models/JourneyModel';


@Component({
    selector: 'app-initiate-journey',
    templateUrl: './initiate-journey.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
    styles: [`
    agm-map {
      height: 400px;
      display: block;
    }
    .collapsible-body{
      display: block
    }
  `],
    styleUrls: ['./initiate-journey.component.css'],
    encapsulation: ViewEncapsulation.None,
})


export class InitiateJourneyComponent implements OnInit {

    journey = {
        isTruckTransport: true, isThirdParty: false, cargoPriority: 0, cargoSeverity: 0
    } as JourneyModel;
    checkpoints: Array<CheckPoint> = [];
    selectedCheckpoint = { id: 1 } as CheckPoint;
    lat: Number = 26.84;
    lng: Number = 26.38;

    origin;
    destination;

    waypoints = [];

    //lat: Number = 41.85
    //lng: Number = -87.65

    //origin = { lat: 29.8174782, lng: -95.6814757 }
    //destination = { lat: 40.6976637, lng: -74.119764 }
    //waypoints = [
    //    { location: { lat: 39.0921167, lng: -94.8559005 } },
    //    { location: { lat: 41.8339037, lng: -87.8720468 } },
    //    {
    //        location: { lat: 28.4813989, lng: -81.5088363 },
    //    },
    //    {
    //        location: { lat: 27.9947147, lng: -82.5943668 },
    //    }, {
    //        location: { lat: 29.9065557, lng: -81.340431 },
    //    }
    //]

    constructor(private mapsAPILoader: MapsAPILoader, private ngZone: NgZone, private journeyService: InitiateJourneyService, private cd: ChangeDetectorRef) { }

    @ViewChild('fromDestinationInput', { static: false }) fromDestinationInput: ElementRef;
    @ViewChild('toDestinationInput', { static: false }) toDestinationInput: ElementRef;
    @ViewChild('checkpointAddressInput', { static: false }) checkpointAddressInput: ElementRef;

    ngOnInit() {
        this.mapsAPILoader.load().then(() => {
            let fromDestination = new google.maps.places.Autocomplete(this.fromDestinationInput.nativeElement, {
                types: ["address"]
            });
            let toDestination = new google.maps.places.Autocomplete(this.toDestinationInput.nativeElement, {
                types: ["address"]
            });
            let checkpointAddress = new google.maps.places.Autocomplete(this.checkpointAddressInput.nativeElement, {
                types: ["address"]
            });

            fromDestination.addListener("place_changed", () => {
                this.ngZone.run(() => {
                    let place: google.maps.places.PlaceResult = fromDestination.getPlace();
                    debugger;
                    if (place.geometry === undefined || place.geometry === null) {
                        return;
                    }
                    else {
                        this.journey.fromDestination = place.formatted_address;
                        this.journey.fromLat = place.geometry.location.lat();
                        this.journey.fromLng = place.geometry.location.lng();

                        this.origin = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };
                        debugger;
                    }

                    if (this.journey.fromLat && this.journey.fromLng && this.journey.toLat && this.journey.toLng)
                        this.getCheckpoints();

                });
            });

            toDestination.addListener("place_changed", () => {
                this.ngZone.run(() => {
                    let place: google.maps.places.PlaceResult = toDestination.getPlace();
                    if (place.geometry === undefined || place.geometry === null) {
                        return;
                    }
                    else {
                        this.journey.toDestination = place.formatted_address;
                        this.journey.toLat = place.geometry.location.lat();
                        this.journey.toLng = place.geometry.location.lng();

                        this.destination = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };
                    }

                    if (this.journey.fromLat && this.journey.fromLng && this.journey.toLat && this.journey.toLng)
                        this.getCheckpoints();

                });
            });

            checkpointAddress.addListener("place_changed", () => {
                this.ngZone.run(() => {
                    let place: google.maps.places.PlaceResult = checkpointAddress.getPlace();
                    if (place.geometry === undefined || place.geometry === null) {
                        return;
                    }
                    else {

                        this.selectedCheckpoint.name = place.formatted_address;
                        this.selectedCheckpoint.lat = place.geometry.location.lat();
                        this.selectedCheckpoint.lng = place.geometry.location.lng();
                        this.checkpoints.push(Object.assign({}, this.selectedCheckpoint));
                        this.selectedCheckpoint.name = "";
                        this.selectedCheckpoint.id++;

                        let waypoint = { location: { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() } };
                        this.waypoints = [...this.waypoints, waypoint];

                        this.cd.detectChanges()

                    }

                });
            });

        });
    }

    //public renderOptions = {
    //    draggable: true,
    //}

    //public change(event: any) {
    //    this.waypoints = event.request.waypoints;
    //}

    deleteCheckpoint(checkpointId) {

        let locToDelete = { location: { lat: 0, lng: 0 } };

        this.checkpoints = this.checkpoints.filter(function (checkpoint) {

            locToDelete = { location: { lat: checkpoint.lat, lng: checkpoint.lng } };
            return checkpoint.id !== checkpointId;
        });

        this.waypoints = this.waypoints.filter(function (waypoint) {
            return waypoint.location.lat != locToDelete.location.lat && waypoint.location.lng != locToDelete.location.lng;
        });

        this.cd.detectChanges()
    }


    getCheckpoints() {
        debugger;
        this.journeyService.getCheckpoints(this.journey.fromLat, this.journey.fromLng, this.journey.toLat, this.journey.toLng).subscribe(result => {
            this.checkpoints = result.data;
        });
    }



    drop(event: CdkDragDrop<string[]>) {
        if (event.previousContainer === event.container) {
            moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
        } else {
            transferArrayItem(event.previousContainer.data,
                event.container.data,
                event.previousIndex,
                event.currentIndex);
        }
    }



    //private setCurrentPosition(lat, lng) {
    //    if ("geolocation" in navigator) {
    //        navigator.geolocation.getCurrentPosition((position) => {
    //            this.journey.fromLat = position.coords.latitude;
    //            this.journey.fronLng = position.coords.longitude;
    //        });
    //    }
    //}


    //public questions: Array<string> = [];

    ////addQuestion() {

    ////}

    //deleteQuestion() {

    //}

    //submitQuestion() {
    //    this.questions.push(this.addQuestion.nativeElement.value);
    //    this.addQuestion.nativeElement.value = "";
    //}

    submitJourney() {
        //console.log(this.initJourney.value)
        //this.JourneyService.initJourney(this.initJourney.value);
    }



    ////public setTimer() {
    ////    this.loading = true;
    ////    this.timer = timer(500);
    ////    this.subscription = this.timer.subscribe(() => {
    ////        this.loading = false;
    ////    });
    ////}


}
