import {Component} from '@angular/core';
import {RouterModule} from '@angular/router';
import {faHome, faSearch, faUser} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";

@Component({
  standalone: true,
  selector: 'app-header',
  imports: [
    RouterModule,
    FontAwesomeModule
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  home = faHome
  search = faSearch
  user = faUser
}
