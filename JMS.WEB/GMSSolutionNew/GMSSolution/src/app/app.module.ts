import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { AppRoutingModule } from './app-routing.module';
import { AgmCoreModule } from '@agm/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './shared/Helpers/jwt.interceptor';
import { ErrorInterceptor } from './shared/Helpers/error.interceptor'
import { AuthGuard } from './shared/Helpers/auth.guard';
import { AgGridModule } from '@ag-grid-community/angular';

//Common
import { AppComponent } from './app.component';
import { CommonService } from "src/app/shared/Services/CommonService";
import { LoginComponent } from './common/login/login.component';
import { ResetPasswordComponent } from './common/reset-password/reset-password.component';
import { MainHeaderComponent } from './common/main-header/main-header.component';
import { NotificationsComponent } from './common/notifications/notifications.component';
import { HomeComponent } from './home/home.component';

//Managers
import { InitiateJourneyComponent } from './manager/initiate-journey/initiate-journey.component';
import { DriverSelectionComponent } from './manager/driver-selection/driver-selection.component';
import { JourneyApprovalComponent } from './manager/journey-approval/journey-approval.component';
import { MyRequestsComponent } from './manager/my-requests/my-requests.component';
import { CurrentJourneysComponent } from './manager/current-journeys/current-journeys.component';
import { JourneyCalendarComponent } from './manager/journey-calendar/journey-calendar.component';
import { JourneyInfoComponent } from './common/journey-info/journey-info.component';
import { JourneyDetailsComponent } from './common/journey-details/journey-details.component';

//Driver
import { JourneyDriverComponent } from './driver/journey/journey-driver.component';
import { DriverAssessmentComponent } from './driver/assessment/driver-assessment.component';
import { JourneyClosureComponent } from './driver/journey-closure/journey-closure.component';

//Admin
import { AdminDashboardComponent } from './admin/dashboard/admin-dashboard.component';
import { UsersManagementComponent } from './admin/users/users-management.component';
import { CheckpointManagementComponent } from './admin/checkpoints/checkpoint-management.component';
import { TeamManagementComponent } from './admin/team/team-management.component';
import { WorkforceManagementComponent } from './admin/workforce/workforce-management.component';
import { ReportsComponent } from './admin/reports/reports.component';



@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        ResetPasswordComponent,
        InitiateJourneyComponent,
        DriverSelectionComponent,
        JourneyApprovalComponent,
        DriverAssessmentComponent,
        JourneyClosureComponent,
        AdminDashboardComponent,
        UsersManagementComponent,
        TeamManagementComponent,
        WorkforceManagementComponent,
        CheckpointManagementComponent,
        ReportsComponent,
        HomeComponent,
        NotificationsComponent,
        JourneyInfoComponent,
        JourneyDetailsComponent,
        JourneyDriverComponent,
        MainHeaderComponent,
        MyRequestsComponent,
        CurrentJourneysComponent,
        JourneyCalendarComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule,
        AgGridModule.withComponents([]),
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
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        CommonService,
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        AuthGuard],
    bootstrap: [AppComponent]
})
export class AppModule { }
