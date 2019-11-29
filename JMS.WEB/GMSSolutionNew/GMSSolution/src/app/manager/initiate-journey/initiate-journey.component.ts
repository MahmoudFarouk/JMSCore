import { OnInit, Component, ChangeDetectionStrategy, ChangeDetectorRef, ViewChild, ElementRef, NgZone } from '@angular/core';
import { MapsAPILoader } from '@agm/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { JourneyModel } from '../../shared/models/JourneyModel';
import { JourneyService } from 'src/app/shared/Services/JourneyService';
import swal from 'sweetalert2';
import { LookupModel } from '../../shared/Models/LookupModel';
import { Checkpoint } from '../../shared/models/CheckpointModel';
import { JourneyUpdate } from '../../shared/models/JourneyUpdateModel';
import { ResponseStatus } from 'src/app/shared/Enums/response-status.enum';

@Component({
  selector: 'app-initiate-journey',
  templateUrl: './initiate-journey.component.html',
  styleUrls: ['./initiate-journey.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
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
  minDate= new Date();
  
  //Egypt Coordinates
  defaultLat: number = 27.61;
  defaultLng: number = 30.32;

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

          if (this.journey.fromLat && this.journey.fromLng && this.journey.toLat && this.journey.toLng)
            this.getCheckpoints();
        });

        checkpointAddress.addListener("place_changed", () => {
          let place: google.maps.places.PlaceResult = checkpointAddress.getPlace();
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }
          else {
            this.selectedCheckpoint = {
              id: 0, name: place.formatted_address, isThirdParty: false, isFromOrTo: false,
              latitude: place.geometry.location.lat(),
              longitude: place.geometry.location.lng(),
              isDeleted: false
            };

            let definedCheckpoint = this.journey.checkpoints
              .find(c => this.selectedCheckpoint.latitude == c.latitude && this.selectedCheckpoint.longitude == c.longitude);

            if (definedCheckpoint) {

              if (definedCheckpoint.isDeleted)
                definedCheckpoint.isDeleted = false;
              else if (definedCheckpoint.isFromOrTo)
                swal.fire("Attention", "You can't add the journey From or To points.", "warning");
              else
                swal.fire("Attention", "Checkpoint Already Exist.", "warning");
            }
            else {
              this.journey.checkpoints.splice(this.journey.checkpoints.length - 1, 0, Object.assign({}, this.selectedCheckpoint));
              this.drawMapCheckpoints();
            }
            this.selectedCheckpoint.name = "";
            this.cd.detectChanges();
          }
        });
      });
    });
  }

  getCheckpoints() {
    this.journeyService.getCheckpoints(this.journey.fromLat, this.journey.fromLng, this.journey.toLat, this.journey.toLng).subscribe(result => {

      this.journey.checkpoints = result.data;

      let fromCheckpoint = {
        latitude: this.journey.fromLat, longitude: this.journey.fromLng,
        name: this.journey.fromDestination, isFromOrTo: true
      } as Checkpoint;

      let toCheckpoint = {
        latitude: this.journey.toLat, longitude: this.journey.toLng,
        name: this.journey.toDestination, isFromOrTo: true
      } as Checkpoint;

      this.journey.checkpoints.unshift(fromCheckpoint);
      this.journey.checkpoints.push(toCheckpoint);

      this.drawMapCheckpoints();
    });
  }

  drawMapCheckpoints() {
    let journey = this.journey;

    this.waypoints = this.journey.checkpoints.filter(function (checkpoint) {
      return !checkpoint.isFromOrTo;
    }).map(function (waypoint) {
      return { location: { lat: waypoint.latitude, lng: waypoint.longitude } }
    });

    this.cd.detectChanges();
  }

  deleteCheckpoint(lat, lng) {

    this.journey.checkpoints = this.journey.checkpoints.filter(function (checkpoint) {
      if (checkpoint.latitude == lat && checkpoint.longitude == lng) {
        checkpoint.isDeleted = true;
      }
      return !checkpoint.isDeleted;
    });
    this.drawMapCheckpoints();
  }

  reArrangeCheckpoints(event: CdkDragDrop<Checkpoint[]>) {
    moveItemInArray(this.journey.checkpoints, event.previousIndex + 1, event.currentIndex + 1);
    this.drawMapCheckpoints();
  }

  getDispatchers() {
    this.journeyService.getDispatchers().subscribe(result => {
      this.dispatchers = result.data;
    });
  }

  submitForm(form) {

    if (form.valid)
      swal.queue([{
        title: 'Initiate Journey',
        text: "Are you sure you want to initiate this Journey?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        showLoaderOnConfirm: true,
        preConfirm: () => {
          return this.journeyService.initJourney(this.journey).subscribe(response => {
            if (response.status == ResponseStatus.Success)
              swal.fire({
                title: 'Success',
                icon: 'success',
                text: 'Journey Intiated Sucessfully.',
                allowOutsideClick: false
              }).then(end => {
                if (end)
                  window.location.href = '.';
              });
          })
        }
      }]);

  }
}


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

     //markerChanged(event: any) {
        //this.waypoints = event.request.waypoints;

        //TODO OnSubmit Take The Final Waypoints into Checkpoints Object
        //for (var i = 0; i < this.journey.checkpoints.length; i++) {
        //    for (var key in this.journey.checkpoints[i]) {

        //        if (this.journey.checkpoints[i].hasOwnProperty(key)) {
        //            //checkpoints[key] = "";
        //        }
        //    }
        //}
    //}
