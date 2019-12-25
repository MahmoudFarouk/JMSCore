import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { AuthenticationService } from '../../shared/Services/AuthenticationService';


@Component({
  selector: 'app-forgetchangepassword',
  templateUrl: './forgetchangepassword.component.html',
  styleUrls: ['./forgetchangepassword.component.css']
})
export class ForgetchangepasswordComponent implements OnInit {
  private token = '';

  submitted = false;
  loginForm: FormGroup;
  loading: boolean = false;
  constructor(private router: Router, private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private activatedRoute: ActivatedRoute) { }
  get f() {
    return this.loginForm.controls;
  }
  async onSubmit() {
  
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }
    
    if (this.f.password.value !== this.f.confirmpassword.value)
      return;
    this.loading = true;
    var result = await this.authenticationService.forgetChangePassword(this.token, this.f.password.value);
    this.loading = false;
    switch (result.status) {
      case 1:
        alert("your password changed successfully");
        this.router.navigate(['/']);
        break;
      default:
        console.log("system error")
        break;
    }
    
  }
showConfirmError(){
  
  return this.f.password.value !== this.f.confirmpassword.value
}
  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      password: ['', Validators.required],
      confirmpassword: ['', Validators.required]

    });
    this.activatedRoute.queryParams.subscribe(params => {
      const token = params['token'];
      this.token = token;

    });
  }

}
