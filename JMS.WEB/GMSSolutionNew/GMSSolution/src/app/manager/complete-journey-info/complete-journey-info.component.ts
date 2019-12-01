import { Component, OnInit, NgModule } from '@angular/core';
import { InitiateJourneyComponent } from 'src/app/manager/initiate-journey/initiate-journey.component';

@NgModule({
  declarations: [InitiateJourneyComponent]
})

@Component({
  selector: 'app-complete-journey-info',
  templateUrl: './complete-journey-info.component.html',
  styleUrls: ['./complete-journey-info.component.css']
})
export class CompleteJourneyInfoComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
