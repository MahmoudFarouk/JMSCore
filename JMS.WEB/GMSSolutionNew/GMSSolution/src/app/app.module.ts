import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { InitiateJourneyComponent } from './initiate-journey/initiate-journey.component';
import { DriverSelectionComponent } from './driver-selection/driver-selection.component';
import { JourneyApprovalComponent } from './journey-approval/journey-approval.component';
import { DriverAssessmentComponent } from './driver-assessment/driver-assessment.component';
import { JourneyClosureComponent } from './journey-closure/journey-closure.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { UsersManagementComponent } from './users-management/users-management.component';
import { TeamManagementComponent } from './team-management/team-management.component';
import { WorkforceManagementComponent } from './workforce-management/workforce-management.component';
import { CheckpointManagementComponent } from './checkpoint-management/checkpoint-management.component';
import { ReportsComponent } from './reports/reports.component';
import { AgmCoreModule } from '@agm/core';
import { HomeComponent } from './home/home.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './shared/Helpers/jwt.interceptor';
import { ErrorInterceptor } from './shared/Helpers/error.interceptor'
import { NotificationsComponent } from './notifications/notifications.component';
import { JourneyInfoComponent } from './shared/Components/journey-info/journey-info.component';
import { JourneyDetailsComponent } from './shared/Components/journey-details/journey-details.component';
import { JourneyDriverComponent } from './journey-driver/journey-driver.component';
import { AuthGuard } from './shared/Helpers/auth.guard';
import { MainHeaderComponent } from './shared/Components/main-header/main-header.component';

import { MyRequestsComponent } from './my-requests/my-requests.component'
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
    MyRequestsComponent
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
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
