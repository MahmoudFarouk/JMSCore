import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgxLoadingModule,ngxLoadingAnimationTypes  } from 'ngx-loading';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CheckpointAssessmentComponent } from './checkpoint-assessment/checkpoint-assessment.component';
import { DriverSelectionComponent } from './driver-selection/driver-selection.component';
import { InitiateJourneyComponent } from './initiate-journey/initiate-journey.component';
import { JourneyApprovalComponent } from './journey-approval/journey-approval.component';
import { JourneyClosureComponent } from './journey-closure/journey-closure.component';
import { JourneyPreparationComponent } from './journey-preparation/journey-preparation.component';
import { JourneyStartingAndMonitoringComponent} from './journey-starting-and-monitoring/journey-starting-and-monitoring.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AgmCoreModule } from '@agm/core';
import { JourneyInfoComponent } from './Components/journey-info/journey-info.component';
import { NotificationsComponent } from './notifications/notifications.component';


@NgModule({
  declarations: [
    AppComponent,CheckpointAssessmentComponent,
    DriverSelectionComponent,InitiateJourneyComponent,
    JourneyApprovalComponent,JourneyClosureComponent,
    JourneyPreparationComponent,JourneyStartingAndMonitoringComponent, JourneyInfoComponent, NotificationsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    AgmCoreModule.forRoot({
      apiKey: "AIzaSyD1mqYsV0ShwvvIaKU3MOr9CJelaCdAb7I",
      libraries: ["places"]
    }),
    ReactiveFormsModule,
    NgxLoadingModule.forRoot({
      animationType: ngxLoadingAnimationTypes.cubeGrid,
      backdropBackgroundColour: 'rgba(0,0,0,0.1)', 
      backdropBorderRadius: '4px',
      primaryColour: '#F5A622', 
      secondaryColour: '#F5A622', 
      tertiaryColour: '#F5A622'
  })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
