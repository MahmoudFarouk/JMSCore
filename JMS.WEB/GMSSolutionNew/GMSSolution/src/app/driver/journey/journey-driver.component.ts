import { Component, OnInit, ChangeDetectorRef, NgZone, ViewChild, ElementRef, } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { JourneyModel } from '../../shared/models/JourneyModel';
import { User } from '../../shared/models/UserModel';
import { AuthenticationService } from '../../shared/Services/AuthenticationService';
import { JourneyService } from '../../shared/Services/JourneyService';
import { DriverService } from '../../shared/Services/DriverService';
import { JourneyStatus } from '../../shared/enums/journey-status.enum';
import { MapsAPILoader, MouseEvent } from '@agm/core';
import swal from "sweetalert2";
declare var $: any;
@Component({
  selector: 'app-journey-driver',
  templateUrl: './journey-driver.component.html',
  styleUrls: ['./journey-driver.component.css']
})
export class JourneyDriverComponent implements OnInit {
  public JourneyId: number = 0;
  journeyId: number;
  journey: JourneyModel = {};
  currentUser: User;
  CheckPoints: any[];
  NextCheckPointId = 0;
  currentCheckPoint: any = { name: '' };
  latitude: number;
  longitude: number;
  zoom: number;
  address: string;
  statusMessage: string = '';
  alertMessage: string = '';
  private geoCoder;
  constructor(private cd: ChangeDetectorRef,
    private authenticationService: AuthenticationService,
    private JourneyService: JourneyService,
    private DriverService: DriverService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone) {
    this.currentUser = this.authenticationService.currentUserValue;

  }
  @ViewChild('search', null)
  public searchElementRef: ElementRef;
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      const journeyId = params['journeyId'];
      this.journeyId = journeyId != undefined && journeyId != null && journeyId != '' ? journeyId : 0;
      this.setGeoLocation();
      this.getJourneyInfo();
    });

  }
  showStartJourney() {
    return this.journey.journeyStatus == JourneyStatus.PendingOnDriverStartJourney;
  }
  showCheckinButton(item) {
    return !this.showStartJourney() && item.id == this.NextCheckPointId;
  }
  showCheckedInButton(item) {
    return !this.showStartJourney() && item.journeyStatus == JourneyStatus.JourneyCompleted;

  }
  getJourneyInfo() {
    this.JourneyService.GetJourneyInfo(this.journeyId).toPromise().then((data: any) => {
      this.journey = data.data;
      this.getCheckPoints();

    }, (error) => {
    });
  }
  getCheckPoints() {

    this.DriverService.GetCheckPointsByJourneyId(this.journeyId).toPromise().then((data: any) => {

      if (data.status == 1)
        this.CheckPoints = data.data;
      for (var i in this.CheckPoints) {
        var item = this.CheckPoints[i];
        if (item.journeyStatus == JourneyStatus.PendingOnDriverCompleteCheckpointAssessment) {
          this.NextCheckPointId = item.id;
          this.currentCheckPoint = item.checkpoint;

          break;
        }
      }
    }, (error) => {
    });
  }
  startJourney() {
    this.JourneyService.UpdateJourneyStatus(this.journeyId, JourneyStatus.PendingOnDriverCompleteCheckpointAssessment).toPromise().then((data: any) => {

      if (data.status == 1) {
        this.journey.journeyStatus = JourneyStatus.PendingOnDriverCompleteCheckpointAssessment;
        this.cd.detectChanges();
      }

    }, (error) => {
    });
  }
  checkin(item) {
    this.router.navigate(['/driver/assessment'], { queryParams: { journeyId: this.journeyId, checkPointId: item.checkpointId, ju: item.id } });
  }
  private setCurrentLocation() {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 15;
        this.getAddress(this.latitude, this.longitude);
      });
    }
  }
  getAddress(latitude, longitude) {

    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {

      if (status === 'OK') {
        if (results[0]) {
          this.zoom = 15;
          this.address = results[0].formatted_address;
        } else {
          //window.alert('No results found');
        }
      } else {
        // window.alert('Geocoder failed due to: ' + status);
      }

    });
  }
  setGeoLocation() {
    this.mapsAPILoader.load().then(() => {
      this.setCurrentLocation();
      this.geoCoder = new google.maps.Geocoder;

      let autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement, {
        types: ["address"]
      });
      autocomplete.addListener("place_changed", () => {
        this.ngZone.run(() => {
          //get the place result
          let place: google.maps.places.PlaceResult = autocomplete.getPlace();

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

  sendLocation() {
    this.JourneyService.AddJourneyUpdate({
      JourneyId: this.journeyId,
      CheckpointId: this.NextCheckPointId,
      StatusMessage: this.address,
      UserId: this.currentUser.id,
      Latitude:this.latitude,
      Longitude:this.longitude
    }).toPromise().then((data) => {
      $(".modal").modal('hide');
      swal.fire("", "Your location sent successfully", "success");
    })

  }
  sendMessage() {
    if (this.statusMessage != '') {
      this.JourneyService.AddJourneyUpdate({
        JourneyId: this.journeyId,
        CheckpointId: this.NextCheckPointId,
        StatusMessage: this.statusMessage,
        UserId: this.currentUser.id,
        IsDriverStatus: true
      }).toPromise().then((data) => {
        $(".modal").modal('hide');
        swal.fire("", "Your message sent successfully", "success");
      });

    } else {
      swal.fire("", "Please, Enter your message", "warning");

    }
  }
  sendAlert() {
    if (this.alertMessage != '') {
      this.JourneyService.AddJourneyUpdate({
        JourneyId: this.journeyId,
        CheckpointId: this.NextCheckPointId,
        StatusMessage: this.alertMessage,
        UserId: this.currentUser.id,
        IsAlert: true
      }).toPromise().then((data) => {
        $(".modal").modal('hide');
        swal.fire("", "Your alert sent successfully", "success");
      });

    } else {
      swal.fire("", "Please, Enter your alert", "warning");

    }
  }
  PauseJourney() {
    swal.fire("", "Your journey paused successfully", "success");

  }
  CloseJourney() {
    swal.fire("", "Your journey closed successfully", "success");

  }

}
