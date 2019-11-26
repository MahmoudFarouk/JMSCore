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
    encapsulation: ViewEncapsulation.None,
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

    //Egypt Coordinates
    defaultLat: number = 26.84;
    defaultLng: number = 26.38;

    origin;
    destination;
    waypoints = [];

    constructor(private mapsAPI: MapsAPILoader, private ngZone: NgZone, private journeyService: JourneyService, private cd: ChangeDetectorRef) { }



    @ViewChild('fromDestinationInput', { read: ElementRef, static: false }) fromDestinationInput: ElementRef;
    @ViewChild('toDestinationInput', { read: ElementRef, static: false }) toDestinationInput: ElementRef;
    @ViewChild('checkpointAddressInput', { read: ElementRef, static: false }) checkpointAddressInput: ElementRef;


    ngOnInit() {

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

                        this.origin = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };
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

                        this.destination = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };
                    }

                    if (this.journey.fromLat && this.journey.fromLng && this.journey.toLat && this.journey.toLng)
                        this.getCheckpoints();

                });

                checkpointAddress.addListener("place_changed", () => {
                    let place: google.maps.places.PlaceResult = checkpointAddress.getPlace();
                    if (place.geometry === undefined || place.geometry === null) {
                        return;
                    }
                    else {

                        this.selectedCheckpoint.id = -1;
                        this.selectedCheckpoint.name = place.formatted_address;
                        this.selectedCheckpoint.latitude = place.geometry.location.lat();
                        this.selectedCheckpoint.longitude = place.geometry.location.lng();
                        this.journey.checkpoints.push(Object.assign({}, this.selectedCheckpoint));
                        this.selectedCheckpoint.name = "";

                        this.drawMapCheckpoints();
                    }

                });
            });
        });
    }

    drawMapCheckpoints() {
        this.waypoints = this.journey.checkpoints.map(function (checkpoint) {
            return { location: { lat: checkpoint.latitude, lng: checkpoint.longitude } }
        });

        this.cd.detectChanges();
    }


    getDispatchers() {
        this.journeyService.getDispatchers().subscribe(result => {
            this.dispatchers = result.data;
        });
    }

    getCheckpoints() {
        this.journeyService.getCheckpoints(this.journey.fromLat, this.journey.fromLng, this.journey.toLat, this.journey.toLng).subscribe(result => {
            this.journey.checkpoints = result.data;
            this.drawMapCheckpoints();
        });
    }

    deleteCheckpoint(checkpointName) {
        this.journey.checkpoints = this.journey.checkpoints.filter(function (checkpoint) {
            return checkpoint.name !== checkpointName;
        });
        this.drawMapCheckpoints();
    }

    reArrangeCheckpoints(event: CdkDragDrop<Checkpoint[]>) {
        moveItemInArray(this.journey.checkpoints, event.previousIndex, event.currentIndex);
        this.drawMapCheckpoints();
    }

    submitForm() {
        this.journeyService.initJourney(this.journey).subscribe(result => {
            if (result)
                swal.fire("Sucess", "Journey Intiated Sucessfully", "success");
        });
    }
}
