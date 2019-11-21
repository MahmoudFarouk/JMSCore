import { Component, OnInit } from '@angular/core';
import { Subscription,Observable,timer } from 'rxjs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/shared/Services/Login/authentication.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loading:boolean = false;
  isCustomComponent:boolean = false;
  isCurrentPage:boolean = false;
  subscription: Subscription;
  timer: Observable<any>;
  loginForm: FormGroup;
  submitted = false;
  returnUrl: string;
  error: string ="";
  
  constructor() {
}
  
  ngOnInit() {
    this.setTimer();
   
 }
 // convenience getter for easy access to form fields
 
 public setTimer(){
  this.loading   = true;
  this.timer = timer(3000);
  this.subscription = this.timer.subscribe(() => {
      this.loading = false;
  });
}

}
