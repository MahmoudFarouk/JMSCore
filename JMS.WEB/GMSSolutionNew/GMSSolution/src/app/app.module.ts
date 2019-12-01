import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { AppRoutingModule } from './app-routing.module';
import { AgmCoreModule, GoogleMapsAPIWrapper } from '@agm/core';
import { AgmDirectionModule } from 'agm-direction';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './shared/Helpers/jwt.interceptor';
import { ErrorInterceptor } from './shared/Helpers/error.interceptor'
import { AuthGuard } from './shared/Helpers/auth.guard';
import { AgGridModule } from '@ag-grid-community/angular';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatRadioModule } from '@angular/material/radio';
import { BlockUIModule } from 'ng-block-ui';
import { BlockUIHttpModule } from 'ng-block-ui/http';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';


//Common
import { AppComponent } from './app.component';
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
import { JourneyMontroing } from './driver/journey-montoring/journey-montoring.component';

//Admin
import { AdminDashboardComponent } from './admin/dashboard/admin-dashboard.component';
import { UsersManagementComponent } from './admin/users/users-management.component';
import { CheckpointManagementComponent } from './admin/checkpoints/checkpoint-management.component';
import { TeamManagementComponent } from './admin/team/team-management.component';
import { WorkforceManagementComponent } from './admin/workforce/workforce-management.component';
import { ReportsComponent } from './admin/reports/reports.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ForgetPasswordComponent } from './common/forget-password/forget-password.component';
import { ForgetchangepasswordComponent } from './common/forgetchangepassword/forgetchangepassword.component';
import { CompleteJourneyInfoComponent } from './manager/complete-journey-info/complete-journey-info.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ResetPasswordComponent,
    InitiateJourneyComponent,
    DriverSelectionComponent,
    JourneyApprovalComponent,
    DriverAssessmentComponent,
    JourneyMontroing,
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
    JourneyCalendarComponent,
    ForgetPasswordComponent,
    ForgetchangepasswordComponent,
    CompleteJourneyInfoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    AgGridModule.withComponents([]),
    BlockUIModule.forRoot(),
    BlockUIHttpModule.forRoot(),
    SweetAlert2Module.forRoot(),
    DragDropModule,
    MatRadioModule,
    AgmDirectionModule,
    AgmCoreModule.forRoot({
      apiKey: "AIzaSyD1mqYsV0ShwvvIaKU3MOr9CJelaCdAb7I",
      libraries: ["places"],
      language: "ar",
      region: "EG"
    }),
    ReactiveFormsModule,
    NgxLoadingModule.forRoot({
      animationType: ngxLoadingAnimationTypes.cubeGrid,
      backdropBackgroundColour: 'rgba(0,0,0,0.1)',
      backdropBorderRadius: '4px',
      primaryColour: '#F5A622',
      secondaryColour: '#F5A622',
      tertiaryColour: '#F5A622'
    }),
    BsDatepickerModule.forRoot(),
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: BsDatepickerConfig, useFactory: getDatepickerConfig },
    AuthGuard,
    GoogleMapsAPIWrapper
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }


export function getDatepickerConfig(): BsDatepickerConfig {

  return Object.assign(new BsDatepickerConfig(), {
    containerClass: "theme-dark-blue"
  });
}
