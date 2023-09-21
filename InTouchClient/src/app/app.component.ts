import {Component, OnInit} from '@angular/core';
import {AuthService} from './core/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'InTouchClient';

  constructor(private authService: AuthService) {
  }

  ngOnInit(): void {
    this.authService.setTokenFromStorage();
    this.authService.isTokenValid().subscribe(res =>{},
      error => {
      if(error.status == 401)
        this.authService.logout()
      })
  }
}
