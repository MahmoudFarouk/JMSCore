import { JourneyService } from './../../shared/Services/User/JourneyService';
import { timer, Observable, Subscription } from 'rxjs';
import { ElementRef, NgZone, OnInit, ViewChild, Component, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MapsAPILoader } from '@agm/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';


@Component({
  selector: 'app-initiate-journey',
  templateUrl: './initiate-journey.component.html',
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

  public latitude: any;
  public longitude: any;
  public latitudeTo: any;
  public longitudeTo: any;
  public zoom: number;
  public origin: any;
  public destination: any;
  public drivingOptions: any = {
    modes: ['BUS'],
  }
  public waypoints: any = []
  public renderOptions = {
    draggable: true,
  } 

  public questions : Array<string> = [];

  
  loading:boolean = false;
  isCustomComponent:boolean = false;
  isCurrentPage:boolean;
  subscription: Subscription;
  timer: Observable<any>;


  @ViewChild("fromDestination",{static:false}) searchElementFrom: ElementRef;
  @ViewChild("toDistination",{static:false}) searchElementTo: ElementRef;
  @ViewChild("addQuestion",{static:false}) addQuestion: ElementRef;



  constructor(
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone,
    private JourneyService: JourneyService
  ) {}

  initJourney = new FormGroup({
      title : new FormControl(),
      fromDestination : new FormControl(),
      fromLat: new FormControl(),
      fromLng: new FormControl(),    
      toLat: new FormControl(),
      toLng: new FormControl(),   
      cargoWeight: new FormControl(),
      cargoPriority: new FormControl(),
      cargoSeverity: new FormControl(),
      cargoType: new FormControl(), 
      toDistination : new FormControl(),
      dateOfDelivery : new FormControl(),
      isTruckTransport : new FormControl(),
      startDate : new FormControl(),
      journeyStatus : new FormControl(),
  });

  submitJourney(){
    console.log(this.initJourney.value)
    this.JourneyService.initJourney(this.initJourney.value);
  }
  
  submitQuestion(){
    this.questions.push(this.addQuestion.nativeElement.value);
    console.log(this.questions);
    console.log(this.addQuestion.nativeElement.value);
    this.addQuestion.nativeElement.value = "";
  }

  
  
  ngOnInit() {
    // console.log(this.searchElementFrom)
    this.isCustomComponent=false;
    this.setTimer();

    this.zoom = 4;
    this.latitude = 39.8282;
    this.longitude = -98.5795;
    this.origin = { lat: this.latitude, lng: this.longitude };

    this.setCurrentPosition();


    this.mapsAPILoader.load().then(() => {
      let fromDestination = new google.maps.places.Autocomplete(this.searchElementFrom.nativeElement, {
        types: ["address"]
      });
      let toDistination = new google.maps.places.Autocomplete(this.searchElementTo.nativeElement, {
        types: ["address"]
      });
      fromDestination.addListener("place_changed", () => {
        this.ngZone.run(() => {
          //get the place result
          let place: google.maps.places.PlaceResult = fromDestination.getPlace();

          //verify result
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }

          //set latitude, longitude and zoom
          this.latitude = place.geometry.location.lat();
          this.longitude = place.geometry.location.lng();
          this.zoom = 12;
          this.initJourney.patchValue({
            fromLat : this.latitude,
            fromLng : this.longitude,
            fromDestination: place.formatted_address
          })
          this.origin = { lat: this.latitude, lng: this.longitude };
        });
        toDistination.addListener("place_changed", ()=>{
          this.ngZone.run(() => {
            //get the place result
            let place: google.maps.places.PlaceResult = toDistination.getPlace();
  
            //verify result
            if (place.geometry === undefined || place.geometry === null) {
              return;
            }
  
            //set latitude, longitude and zoom
            this.latitudeTo = place.geometry.location.lat();
            this.longitudeTo = place.geometry.location.lng();
            this.zoom = 12;
            this.initJourney.patchValue({
              toLat : this.latitudeTo,
              toLng : this.longitudeTo,
              toDistination: place.formatted_address,
            })  
            this.destination = { lat: this.latitudeTo, lng: this.longitudeTo };
          });
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
  public change(event: any) {
    this.waypoints = event.request.waypoints;
  }

}

