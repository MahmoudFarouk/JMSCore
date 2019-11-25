import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../shared/Services/Login/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.css']
})
export class ForgetPasswordComponent implements OnInit {

  submitted = false;
  loginForm: FormGroup;
  loading:boolean=false;
  constructor(private formBuilder: FormBuilder, private authenticationService: AuthenticationService) { }
  get f() {
    return this.loginForm.controls;
  }
  async onSubmit() {
    debugger;
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }
    this.loading=true;
    var result=await this.authenticationService.forgetPassword(this.f.username.value);
    this.loading=false;
    switch(result.status)
    {
      case 1:
       alert("Please, check your mail to reset to password");
      break;
      case 6:
      alert("Email incorrect")
      break;
      default:
      console.log("system error")
      break;
    }
    debugger;
  }   
  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],

    });
  }
  
} 
