import { Component, OnInit } from '@angular/core';
import { Subscription,Observable,timer } from 'rxjs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../../shared/Services/AuthenticationService';


@Component({ 
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    ShowLoginThings = true;
  loading:boolean = false;
  isCustomComponent:boolean = false;
  isCurrentPage:boolean = false;
  subscription: Subscription;
  timer: Observable<any>;
  loginForm: FormGroup;
  submitted = false;
  returnUrl: string;
  error: string ="";
  constructor(
        private authenticationService: AuthenticationService,
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router
  ) {
// redirect to home if already logged in
if (this.authenticationService.currentUserValue) { 
  this.router.navigate(['/']);
}
  }
  ngOnInit() {
    this.setTimer();
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
  });

  // get return url from route parameters or default to '/'
  this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
 }
 // convenience getter for easy access to form fields
 get f() {
  return this.loginForm.controls;
 }
 onSubmit() {
     this.submitted = true;

     // stop here if form is invalid
     if (this.loginForm.invalid) {
         return;
     }

     this.loading = true;
     this.authenticationService.login(this.f.username.value, this.f.password.value)
         .pipe(first())
         .subscribe(
             data => {
                 this.router.navigate([this.returnUrl]);
             },
             error => {
                 this.error = error;
                 this.loading = false;
             });
 }
 public setTimer(){
  this.loading   = true;
  this.timer = timer(3000);
  this.subscription = this.timer.subscribe(() => {
      this.loading = false;
  });
}

}
