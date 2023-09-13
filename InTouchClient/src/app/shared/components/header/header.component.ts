import {Component} from '@angular/core';
import {RouterModule} from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-header',
  imports: [
    RouterModule
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

}
