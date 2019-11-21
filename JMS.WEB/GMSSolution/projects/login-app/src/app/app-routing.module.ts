import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './../../../user-app/src/app/app.component';

const routes: Routes = [
  {path:'',component:LoginComponent},
  {path:'Home',component:AppComponent},
  {path:'Register',component:RegisterComponent},
  {path:'**',component:LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
