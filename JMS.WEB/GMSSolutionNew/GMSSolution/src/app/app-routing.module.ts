import { DriverAssessmentComponent } from './driver-assessment/driver-assessment.component';
import { JourneyApprovalComponent } from './journey-approval/journey-approval.component';
import { DriverSelectionComponent } from './driver-selection/driver-selection.component';
import { InitiateJourneyComponent } from './initiate-journey/initiate-journey.component';
import { ReportsComponent } from './reports/reports.component';
import { WorkforceManagementComponent } from './workforce-management/workforce-management.component';
import { TeamManagementComponent } from './team-management/team-management.component';
import { UsersManagementComponent } from './users-management/users-management.component';
import { CheckpointManagementComponent } from './checkpoint-management/checkpoint-management.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { JourneyClosureComponent } from './journey-closure/journey-closure.component';
import { AppComponent } from './app.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './shared/Helpers/auth.guard';
import { HomeComponent } from './home/home.component';
import { NotificationsComponent } from './notifications/notifications.component';
import { JourneyDetailsComponent } from './shared/Components/journey-details/journey-details.component';
import { JourneyDriverComponent } from './journey-driver/journey-driver.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [AuthGuard]
  },
  { path: 'login', component: LoginComponent },
  { path: 'ResetPassword', component: ResetPasswordComponent },
  {
    path: 'InitiateJourney',
    component: InitiateJourneyComponent,
    canActivate: [AuthGuard]
  },
  { path: 'DriverSelection', component: DriverSelectionComponent,
  canActivate: [AuthGuard]
},
  { path: 'JourneyApproval', component: JourneyApprovalComponent },
  { path: 'DriverAssessment', component: DriverAssessmentComponent },
  { path: 'JourneyClosure', component: JourneyClosureComponent },
  { path: 'AdminDashboard', component: AdminDashboardComponent,canActivate: [AuthGuard] },
  { path: 'UsersManagement', component: UsersManagementComponent },
  { path: 'TeamManagement', component: TeamManagementComponent },
  { path: 'WorkforceManagement', component: WorkforceManagementComponent },
  { path: 'CheckpointManagement', component: CheckpointManagementComponent },
  { path: 'Reports', component: ReportsComponent },
  { path: 'Notifications', component: NotificationsComponent,canActivate: [AuthGuard] },
  { path: 'JourneyDetails', component: JourneyDetailsComponent,canActivate: [AuthGuard] },
  { path: 'JourneyDriver', component: JourneyDriverComponent,canActivate: [AuthGuard] },


  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
