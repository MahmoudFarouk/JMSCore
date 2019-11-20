import { CheckpointAssessmentComponent } from './checkpoint-assessment/checkpoint-assessment.component';
import { JourneyStartingAndMonitoringComponent } from './journey-starting-and-monitoring/journey-starting-and-monitoring.component';
import { JourneyPreparationComponent } from './journey-preparation/journey-preparation.component';
import { JourneyApprovalComponent } from './journey-approval/journey-approval.component';
import { DriverSelectionComponent } from './driver-selection/driver-selection.component';
import { InitiateJourneyComponent } from './initiate-journey/initiate-journey.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { JourneyClosureComponent } from './journey-closure/journey-closure.component';
import { AppComponent } from './app.component';
import { NotificationsComponent } from './notifications/notifications.component';


const routes: Routes = [
  {path:'',component:AppComponent},
  {path:'InitiateJourney',component:InitiateJourneyComponent},
  {path:'DriverSelection',component:DriverSelectionComponent},
  {path:'JourneyApproval',component:JourneyApprovalComponent},
  {path:'JourneyPreparation',component:JourneyPreparationComponent},
  {path:'JourneyStartingAndMonitoring',component:JourneyStartingAndMonitoringComponent},
  {path:'CheckpointAssessment',component:CheckpointAssessmentComponent},
  {path:'JourneyClosure',component:JourneyClosureComponent},
  {path:'Notifications',component:NotificationsComponent},
  {path:'**',component:InitiateJourneyComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
