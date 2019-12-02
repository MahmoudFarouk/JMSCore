import { OnInit, Component, ChangeDetectionStrategy, ChangeDetectorRef, ViewChild, ElementRef, NgZone } from '@angular/core';
import { MapsAPILoader } from '@agm/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { JourneyModel } from '../../shared/models/JourneyModel';
import { JourneyService } from 'src/app/shared/Services/JourneyService';
import swal from 'sweetalert2';
import { LookupModel } from '../../shared/Models/LookupModel';
import { Checkpoint } from '../../shared/models/CheckpointModel';
import { ResponseStatus } from 'src/app/shared/Enums/response-status.enum';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/shared/Services/AuthenticationService';
import { AssessmentQuestion } from '../../shared/models/AssessmentQuestionModel';
import { QuestionCategory } from '../../shared/enums/question-category.enum';
import { JourneyStatus } from 'src/app/shared/enums/journey-status.enum';
import { ignoreElements } from 'rxjs/operators';


@Component({
  selector: 'app-initiate-journey',
  templateUrl: './initiate-journey.component.html',
  styleUrls: ['./initiate-journey.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})


export class InitiateJourneyComponent implements OnInit {

  currentUserRole: string;

  constructor(
    private mapsAPI: MapsAPILoader,
    private ngZone: NgZone,
    private journeyService: JourneyService,
    private cd: ChangeDetectorRef,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) {
    this.currentUserRole = this.authenticationService.currentUserValue.roles[0].name;
  }

  pageTitle: string;
  isEditMode: boolean = false;
  isValidateMode: boolean = false;
  isReadOnly: boolean = false;
  question: any[] = [];
  categories: number[] = [1, 2, 3];
  questionId: number = 0;

  journey = {} as JourneyModel;

  //Egypt Coordinates
  defaultLat: number = 27.61;
  defaultLng: number = 30.32;

  dispatchers: LookupModel[] = [];
  selectedCheckpoint = {} as Checkpoint;
  minDate = new Date();

  fromDestination;
  toDestination;
  waypoints = [];

  journeyId: string;


  @ViewChild('fromDestinationInput', { read: ElementRef, static: false }) fromDestinationInput: ElementRef;
  @ViewChild('toDestinationInput', { read: ElementRef, static: false }) toDestinationInput: ElementRef;
  @ViewChild('checkpointAddressInput', { read: ElementRef, static: false }) checkpointAddressInput: ElementRef;

  ngOnInit() {

    if (this.route.routeConfig.path.indexOf("initiate-journey") != -1) {

      this.pageTitle = "Initiate Journey";
      this.journey = {
        isTruckTransport: true,
        isThirdParty: false,
        cargoPriority: 0,
        cargoSeverity: 0,
        checkpoints: []
      };

      this.isEditMode = true;
      this.getDispatchers();
    }
    else if (this.route.routeConfig.path.indexOf("validate-journey") != -1) {

      this.pageTitle = "Validate Journey";

      this.journeyId = this.route.snapshot.paramMap.get("id");

      if (this.journeyId) {
        this.journeyService.getAllJourneyInfo(this.journeyId).subscribe(response => {
          this.journey = response;
          this.journey.deliveryDate = new Date(response.deliveryDate);
          this.journey.assessmentQuestion = [];
          this.addDistination(this.journey.fromDestination, this.journey.fromLat, this.journey.fromLng, true);
          this.addDistination(this.journey.toDestination, this.journey.toLat, this.journey.toLng, false);
          this.isValidateMode = true;
          this.getCheckpoints();
        });
      }
    }
    else {
      this.pageTitle = "Journey Details";

      this.journeyId = this.route.snapshot.paramMap.get("id");
      this.journeyService.getAllJourneyInfo(this.journeyId).subscribe(response => {
        this.journey = response;
        this.journey.deliveryDate = new Date(response.deliveryDate);
        this.addDistination(this.journey.fromDestination, this.journey.fromLat, this.journey.fromLng, true);
        this.addDistination(this.journey.toDestination, this.journey.toLat, this.journey.toLng, false);
        this.isReadOnly = true;
        this.getCheckpoints();
      });;
    }

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
            this.addDistination(place.formatted_address, place.geometry.location.lat(), place.geometry.location.lng(), true)
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
            this.addDistination(place.formatted_address, place.geometry.location.lat(), place.geometry.location.lng(), false)
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

  addDistination(name, lat, lng, isFrom) {
    if (isFrom) {
      this.journey.fromDestination = name;
      this.journey.fromLat = lat;
      this.journey.fromLng = lng;
      this.fromDestination = { lat: lat, lng: lng };
    }
    else {
      this.journey.toDestination = name;
      this.journey.toLat = lat;
      this.journey.toLng = lng;
      this.toDestination = { lat: lat, lng: lng };
    }
  }

  getCheckpoints() {
    if (this.isEditMode)
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
    else {
      this.journey.checkpoints[0].isFromOrTo = true;
      this.journey.checkpoints[this.journey.checkpoints.length - 1].isFromOrTo = true;
      this.drawMapCheckpoints();
    }


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

  addQuestion(category: number, checkpointId?: number) {
    let currentQuestion = {} as AssessmentQuestion;

    if (this.question[category] != "") {
      currentQuestion = {
        question: this.question[category],
        category: category,
        isThirdParty: this.journey.isThirdParty,
        id: ++this.questionId
      } as AssessmentQuestion;

      this.question[category] = "";
    }
    else if (this.question[checkpointId] != "") {
      currentQuestion = {
        question: this.question[checkpointId],
        category: category,
        isThirdParty: this.journey.isThirdParty,
        checkpointId: checkpointId,
        id: ++this.questionId
      } as AssessmentQuestion;

      this.question[checkpointId] = "";
    }

    if (currentQuestion.question)
      this.journey.assessmentQuestion.push(currentQuestion);


  }



  deleteQuestion(questionId) {
    this.journey.assessmentQuestion = this.journey.assessmentQuestion.filter(function (question) {
      return questionId != question.id;
    });
  }



  submitForm(form) {

    if (form.valid || this.isValidateMode)
      swal.queue([{
        title: this.isValidateMode ? 'Submit Journey' : 'Initiate Journey',
        text: this.isValidateMode ? "Are you sure you want to submit this Journey?" : "Are you sure you want to initiate this Journey?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        showLoaderOnConfirm: true,
        preConfirm: () => {
          if (this.isValidateMode) {

            //TODO: UpdateJourney
            this.journey.journeyStatus = JourneyStatus.PendingOnDriverSelection;
            return this.journeyService.validateJourney(this.journey).subscribe(response => {
              if (response.status == ResponseStatus.Success)
                swal.fire({
                  title: 'Journey Submitted Sucessfully',
                  icon: 'success',
                  text: 'Proceed to Driver Selection.',
                  allowOutsideClick: false
                }).then(end => {
                  if (end)
                    this.router.navigate([`/driver-selection`], { queryParams: { journeyId: response.message } });
                });
            })
          } else {
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
        }
      }]);

  }

  viewUpdates() {
    this.router.navigate([`/journeymontoring/`], { queryParams: { journeyId: this.journeyId } });
  }

  getResult(question: AssessmentQuestion) {
    let result = question.assessmentResult;
    if (!this.isEditMode && result &&result[0])
      if (result[0].isYes)
        return "Yes"
      else
        return "No"
    else return "Not Answered Yet";

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
