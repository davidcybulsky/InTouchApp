import { Component, OnInit } from '@angular/core';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'InTouchClient';

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    console.log("INIT")
    this.authService.setTokenFromStorage();
  }
}
