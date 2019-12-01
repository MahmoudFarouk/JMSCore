import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './shared/Helpers/auth.guard';

//Common
import { AppComponent } from './app.component';
import { LoginComponent } from './common/login/login.component';
import { ResetPasswordComponent } from './common/reset-password/reset-password.component';
import { NotificationsComponent } from './common/notifications/notifications.component';
import { HomeComponent } from './home/home.component';

//Managers
import { InitiateJourneyComponent } from './manager/initiate-journey/initiate-journey.component';
import { CompleteJourneyInfoComponent } from './manager/complete-journey-info/complete-journey-info.component';
import { JourneyApprovalComponent } from './manager/journey-approval/journey-approval.component';
import { DriverSelectionComponent } from './manager/driver-selection/driver-selection.component';
import { MyRequestsComponent } from './manager/my-requests/my-requests.component';
import { CurrentJourneysComponent } from './manager/current-journeys/current-journeys.component';
import { JourneyCalendarComponent } from './manager/journey-calendar/journey-calendar.component';
import { JourneyDetailsComponent } from './common/journey-details/journey-details.component';

//Drivers
import { DriverAssessmentComponent } from './driver/assessment/driver-assessment.component';
import { JourneyClosureComponent } from './driver/journey-closure/journey-closure.component';
import { JourneyDriverComponent } from './driver/journey/journey-driver.component';

//Admin
import { AdminDashboardComponent } from './admin/dashboard/admin-dashboard.component';
import { WorkforceManagementComponent } from './admin/workforce/workforce-management.component';
import { TeamManagementComponent } from './admin/team/team-management.component';
import { UsersManagementComponent } from './admin/users/users-management.component';
import { CheckpointManagementComponent } from './admin/checkpoints/checkpoint-management.component';
import { ReportsComponent } from './admin/reports/reports.component';
import { ForgetPasswordComponent } from './common/forget-password/forget-password.component';
import { ForgetchangepasswordComponent } from './common/forgetchangepassword/forgetchangepassword.component';


const routes: Routes = [
    {
        path: '',
        component: HomeComponent,
        canActivate: [AuthGuard]
    },
    { path: 'login', component: LoginComponent },
    { path: 'forgetpassword', component: ForgetPasswordComponent },
    { path: 'reset-password', component: ResetPasswordComponent },
    { path: 'forgetchangepassword', component: ForgetchangepasswordComponent },


    //App Routes
    { path: 'initiate-journey', component: InitiateJourneyComponent, canActivate: [AuthGuard] },
    { path: 'validate-journey/:id', component: InitiateJourneyComponent, canActivate: [AuthGuard] },
    { path: 'requests', component: MyRequestsComponent, canActivate: [AuthGuard] },
    { path: 'journeys', component: CurrentJourneysComponent, canActivate: [AuthGuard] },
    { path: 'journey-calendar', component: JourneyCalendarComponent, canActivate: [AuthGuard] },
    { path: 'driver/journey', component: JourneyDriverComponent, canActivate: [AuthGuard] },

    { path: 'driver-selection', component: DriverSelectionComponent, canActivate: [AuthGuard] },
    { path: 'journeyapproval', component: JourneyApprovalComponent },
    { path: 'driver/assessment', component: DriverAssessmentComponent, canActivate: [AuthGuard] },
    { path: 'journeyclosure', component: JourneyClosureComponent },
    { path: 'checkpointmanagement', component: CheckpointManagementComponent },
    { path: 'notifications', component: NotificationsComponent, canActivate: [AuthGuard] },
    { path: 'journeydetails', component: JourneyDetailsComponent, canActivate: [AuthGuard] },
    { path: 'journeydriver', component: JourneyDriverComponent, canActivate: [AuthGuard] },

    //Admin Routes
    { path: 'admin/dashboard', component: AdminDashboardComponent, canActivate: [AuthGuard] },
    { path: 'admin/users', component: UsersManagementComponent, canActivate: [AuthGuard] },
    { path: 'admin/checkpoints', component: CheckpointManagementComponent, canActivate: [AuthGuard] },
    { path: 'admin/teams', component: TeamManagementComponent, canActivate: [AuthGuard] },
    { path: 'admin/workforces', component: WorkforceManagementComponent, canActivate: [AuthGuard] },
    { path: 'admin/reports', component: ReportsComponent, canActivate: [AuthGuard] },


    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
