import { OnInit, Component, ChangeDetectionStrategy, ChangeDetectorRef, AfterViewInit, ViewEncapsulation, ViewChild, ElementRef, NgZone } from '@angular/core';
import { MapsAPILoader } from '@agm/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { JourneyModel } from '../../shared/models/JourneyModel';
import { JourneyService } from 'src/app/shared/Services/JourneyService';
import swal from "sweetalert2";
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { swalProviderToken } from '@sweetalert2/ngx-sweetalert2/lib/di';
import { LookupModel } from '../../shared/Models/LookupModel';
import { Checkpoint } from '../../shared/models/CheckpointModel';
import { JourneyUpdate } from '../../shared/models/JourneyUpdateModel';


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
    //encapsulation: ViewEncapsulation.None,
})


export class InitiateJourneyComponent implements OnInit {

    journey = {
        isTruckTransport: true,
        isThirdParty: false,
        cargoPriority: 0,
        cargoSeverity: 0,
        checkpoints: []
    } as JourneyModel;

    dispatchers: LookupModel[] = [];
    selectedCheckpoint = {} as Checkpoint;
    predefinedCheckpoints = [] as Checkpoint[];

    //Egypt Coordinates
    defaultLat: number = 26.84;
    defaultLng: number = 26.38;
    //public renderOptions = { suppressMarkers: false };
    //markerOptions = {
    //    origin: {
    //        //draggable: true,
    //        infoWindow: `<h4>Start Point<h4>`,
    //        icon: 'https://img.icons8.com/cotton/64/000000/place-marker.png'
    //    },
    //    waypoints: {
    //        infoWindow: `<h4>Check Point<h4>`,
    //        icon:'https://i.imgur.com/7teZKif.png',
    //    },
    //    destination: {
    //        //draggable: true,
    //        infoWindow: `<h4>End Point<h4>`,
    //        icon: 'https://img.icons8.com/cotton/64/000000/pin3.png',
    //    }
    //}

    fromDestination;
    toDestination;
    waypoints = [];

    constructor(private mapsAPI: MapsAPILoader, private ngZone: NgZone, private journeyService: JourneyService, private cd: ChangeDetectorRef) { }


    @ViewChild('fromDestinationInput', { read: ElementRef, static: false }) fromDestinationInput: ElementRef;
    @ViewChild('toDestinationInput', { read: ElementRef, static: false }) toDestinationInput: ElementRef;
    @ViewChild('checkpointAddressInput', { read: ElementRef, static: false }) checkpointAddressInput: ElementRef;


    ngOnInit() {

        this.getDispatchers();

        this.mapsAPI.load().then(() => {
            let fromDestination = new google.maps.places.Autocomplete(this.fromDestinationInput.nativeElement, {});
            let toDestination = new google.maps.places.Autocomplete(this.toDestinationInput.nativeElement, {});
            let checkpointAddress = new google.maps.places.Autocomplete(this.checkpointAddressInput.nativeElement, {});

            this.ngZone.run(() => {

                fromDestination.addListener("place_changed", () => {
                    let place: google.maps.places.PlaceResult = fromDestination.getPlace();
                    if (place.geometry === undefined || place.geometry === null) {
                        this.journey.fromDestination = "";
                        return;
                    }
                    else {
                        this.journey.fromDestination = place.formatted_address;
                        this.journey.fromLat = place.geometry.location.lat();
                        this.journey.fromLng = place.geometry.location.lng();

                        this.fromDestination = { lat: this.journey.fromLat, lng: this.journey.fromLng };
                    }

                    if (this.journey.fromLat && this.journey.fromLng && this.journey.toLat && this.journey.toLng)
                        this.getCheckpoints();

                });

                toDestination.addListener("place_changed", () => {
                    let place: google.maps.places.PlaceResult = toDestination.getPlace();
                    if (place.geometry === undefined || place.geometry === null) {
                        this.journey.toDestination = "";
                        return;
                    }
                    else {

                        this.journey.toDestination = place.formatted_address;
                        this.journey.toLat = place.geometry.location.lat();
                        this.journey.toLng = place.geometry.location.lng();

                        this.toDestination = { lat: this.journey.toLat, lng: this.journey.toLng };
                    }

                    this.cd.detectChanges();


                    if (this.journey.fromLat && this.journey.fromLng && this.journey.toLat && this.journey.toLng)
                        this.getCheckpoints();

                });

                checkpointAddress.addListener("place_changed", () => {
                    let place: google.maps.places.PlaceResult = checkpointAddress.getPlace();
                    if (place.geometry === undefined || place.geometry === null) {
                        return;
                    }
                    else {


                        this.selectedCheckpoint.id = 0;
                        this.selectedCheckpoint.name = place.formatted_address;
                        this.selectedCheckpoint.latitude = place.geometry.location.lat();
                        this.selectedCheckpoint.longitude = place.geometry.location.lng();

                        let filteredCheckpoints = this.journey.checkpoints.filter(c => (this.selectedCheckpoint.latitude == c.latitude && this.selectedCheckpoint.longitude == c.longitude));
                        let filteredPredefined = this.predefinedCheckpoints.filter(c => (this.selectedCheckpoint.latitude == c.latitude && this.selectedCheckpoint.longitude == c.longitude));

                        if (filteredCheckpoints.length > 0 || filteredPredefined.length > 0 ||
                            (this.selectedCheckpoint.latitude == this.journey.fromLat && this.selectedCheckpoint.longitude == this.journey.fromLng) ||
                            (this.selectedCheckpoint.latitude == this.journey.toLat && this.selectedCheckpoint.longitude == this.journey.toLng)) {

                            if (filteredPredefined.length>0)
                            {
                                this.journey.checkpoints.push(Object.assign({}, filteredPredefined[0]));
                                this.predefinedCheckpoints = this.predefinedCheckpoints.filter(c => c != filteredPredefined[0]);
                            }
                            else
                                swal.fire("Attention", "Checkpoint Already Exist", "warning");
                        }
                        else {
                            this.journey.checkpoints.push(Object.assign({}, this.selectedCheckpoint));
                            this.drawMapCheckpoints();
                        }
                        this.selectedCheckpoint.name = "";
                        this.cd.detectChanges();
                    }
                });
            });
        });
    }

    markerChanged(event: any) {
        //this.waypoints = event.request.waypoints;

        ////TODO OnSubmit Take The Final Waypoints into Checkpoints Object
        //for (var i = 0; i < this.journey.checkpoints.length; i++) {
        //    for (var key in this.journey.checkpoints[i]) {

        //        if (this.journey.checkpoints[i].hasOwnProperty(key)) {
        //            //checkpoints[key] = "";
        //        }
        //    }
        //}
    }

    getCheckpoints() {
        this.journeyService.getCheckpoints(this.journey.fromLat, this.journey.fromLng, this.journey.toLat, this.journey.toLng).subscribe(result => {
            this.journey.checkpoints = result.data;
            this.predefinedCheckpoints = [];
            this.drawMapCheckpoints();
        });
    }

    drawMapCheckpoints() {
        this.waypoints = this.journey.checkpoints.filter(function (checkpoint) {
            return !checkpoint.isDeleted;
        }).map(function (checkpoint) { return { location: { lat: checkpoint.latitude, lng: checkpoint.longitude } } });

        this.waypoints = this.journey.checkpoints.map(function (checkpoint) { return { location: { lat: checkpoint.latitude, lng: checkpoint.longitude } } });

        this.cd.detectChanges();
    }

    deleteCheckpoint(lat, lng) {
        let predefinedCheckpoints = this.predefinedCheckpoints

        this.journey.checkpoints = this.journey.checkpoints.filter(function (checkpoint) {
            if (checkpoint.latitude == lat && checkpoint.longitude == lng && checkpoint.id != -1) {
                predefinedCheckpoints.push(checkpoint);
            }
            return checkpoint.latitude != lat && checkpoint.longitude != lng;
        });
        this.drawMapCheckpoints();
    }

    reArrangeCheckpoints(event: CdkDragDrop<Checkpoint[]>) {
        moveItemInArray(this.journey.checkpoints, event.previousIndex, event.currentIndex);
        this.drawMapCheckpoints();
    }

    getDispatchers() {
        this.journeyService.getDispatchers().subscribe(result => {
            this.dispatchers = result.data;
        });
    }

    submitForm() {

        swal.fire({
            title: 'Initiate Journey',
            text: "Are you sure you want to initiate this Journey",
            icon: 'warning',
            showCancelButton: true,
            //confirmButtonColor: '#3085d6',
            //cancelButtonColor: '#d33',
            confirmButtonText: 'Yes'
        }).then((result) => {
            this.journeyService.initJourney(this.journey).subscribe(result => {
                if (result)
                    swal.fire("Sucess", "Journey Intiated Sucessfully", "success");
            });
        });
    }
}
