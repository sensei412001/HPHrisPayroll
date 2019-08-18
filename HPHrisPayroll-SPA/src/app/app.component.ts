import { Component, OnInit } from '@angular/core';
import config from 'devextreme/core/config';
import { AuthService } from './_services/auth.service';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from './_model/userModel';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService();

  constructor(
    private authService: AuthService,
    private router: Router) {}

  ngOnInit() {
    config({
      rtlEnabled: false,
      forceIsoDateParsing: true,
      defaultCurrency: 'Php'
    });

    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));

    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
    if (user) {
      this.authService.currentUser = user;
    }
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
