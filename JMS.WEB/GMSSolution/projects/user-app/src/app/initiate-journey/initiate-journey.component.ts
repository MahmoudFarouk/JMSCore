import { Component, OnInit, ElementRef, ViewChild, NgZone } from '@angular/core';
import { Subscription,Observable,timer } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';
//import { } from 'googlemaps';
import { MapsAPILoader } from '@agm/core';

@Component({
  selector: 'app-initiate-journey',
  templateUrl: './initiate-journey.component.html',
  styles: [`
    agm-map {
      height: 400px;
      display: block
    }
  `],
  styleUrls: ['./initiate-journey.component.css']
})
export class InitiateJourneyComponent implements OnInit {
  loading:boolean = false;
  isCustomComponent:boolean = false;
  isCurrentPage:boolean;
  subscription: Subscription;
  timer: Observable<any>;

  public latitude: number;
  public longitude: number;
  public searchControl: FormControl;
  public zoom: number;

  // @ViewChild("search");

  public searchElementRef: ElementRef;

  constructor(
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone

  ) { }

  initJourney = new FormGroup({
    journeyDetails: new FormGroup({
      title : new FormControl(),
      DestinationFrom : new FormControl(),
      DestinationTo : new FormControl(),
      dateOfDelivery : new FormControl()
    }),
    cargoDetails: new FormGroup({
      Type : new FormControl(),
      Weight : new FormControl()
    }),
    inspectionChecklist: new FormGroup({
      LocationFrom : new FormControl(),
      LocationTo : new FormControl(),
      PLShipping : new FormControl(),
      PLReceiving : new FormControl(),
      DateAndTime : new FormControl(),
      ConductedBy : new FormControl(),
    }),
    addJourneyAssessment: new FormGroup({

    }),
    addVehicleAssessment: new FormGroup({

    }),
  });

  submitJourney(){
    console.log(this.initJourney.value)
  }
  
  ngOnInit() {
    this.isCustomComponent=false;
    this.setTimer();

    this.zoom = 4;
    this.latitude = 39.8282;
    this.longitude = -98.5795;
    
    this.searchControl = new FormControl();
    this.setCurrentPosition();

    this.mapsAPILoader.load().then(() => {
      let autocomplete:any ={};/* new google.maps.places.Autocomplete(this.searchElementRef.nativeElement, {
        types: ["address"]
      });*/
      autocomplete.addListener("place_changed", () => {
        this.ngZone.run(() => {
          //get the place result
          let place:any;// google.maps.places.PlaceResult = autocomplete.getPlace();

          //verify result
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }

          //set latitude, longitude and zoom
          this.latitude = place.geometry.location.lat();
          this.longitude = place.geometry.location.lng();
          this.zoom = 12;
        });
      });
    });

 }
  public setTimer(){
    this.loading   = true;
    this.timer = timer(500);
    this.subscription = this.timer.subscribe(() => {
        this.loading = false;
    });
  }

  private setCurrentPosition() {
    if ("geolocation" in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 12;
      });
    }
  }


}
