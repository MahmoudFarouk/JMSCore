import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../../shared/Services/AuthenticationService';
import swal from "sweetalert2";
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
   
    swal.fire({
      title: 'Are you sure to reset your password?',
      text: "",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, reset it!'
    }).then(async (result1) => {
      if (result.value) {
        this.loading=true;
        var result=await this.authenticationService.forgetPassword(this.f.username.value);
        this.loading=false;
        switch(result.status)
        {
          case 1:
          swal.fire("", "Please, check your mail to reset to password","success");
          break;
          case 6:
          swal.fire("", "Email incorrect","error");
          break;
          default:
          console.log("system error")
          break;
        }
      }
    })
    
    debugger;
  }   
  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],

    });
  }
  
} 
