import { Component,OnInit } from '@angular/core';
import { User } from 'src/app/shared/Entities/Login/user';
import { Router } from '@angular/router';
import { AuthenticationService } from './shared/Services/Login/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
 
  title = 'GMS';
  currentUser: User;
 
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);

  }

  ngOnInit(): void {
    
  }


  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }
}
