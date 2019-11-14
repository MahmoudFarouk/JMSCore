import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { CheckpointManagementComponent } from './checkpoint-management/checkpoint-management.component';
import { ReportsComponent } from './reports/reports.component';
import { TeamManagementComponent } from './team-management/team-management.component';
import { UsersManagementComponent } from './users-management/users-management.component';
import { WorkforceManagementComponent } from './workforce-management/workforce-management.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminDashboardComponent,CheckpointManagementComponent,
    ReportsComponent,TeamManagementComponent,
    UsersManagementComponent,WorkforceManagementComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
