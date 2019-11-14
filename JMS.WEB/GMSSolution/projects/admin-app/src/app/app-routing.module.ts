import { ReportsComponent } from './reports/reports.component';
import { WorkforceManagementComponent } from './workforce-management/workforce-management.component';
import { TeamManagementComponent } from './team-management/team-management.component';
import { UsersManagementComponent } from './users-management/users-management.component';
import { CheckpointManagementComponent } from './checkpoint-management/checkpoint-management.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {path:'',component:AdminDashboardComponent},
  {path:'UsersManagement',component:UsersManagementComponent},
  {path:'TeamManagement',component:TeamManagementComponent},
  {path:'WorkforceManagement',component:WorkforceManagementComponent},
  {path:'CheckpointManagement',component:CheckpointManagementComponent},
  {path:'Reports',component:ReportsComponent},
  {path:'**',component:AdminDashboardComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
