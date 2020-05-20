import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { AlertifyService } from '../shared/services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in successfully');
    }, error => {
      this.alertify.error(error);
    });
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

  getUserName() {
    return this.authService.getUserName();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('Logged out');
  }

}
