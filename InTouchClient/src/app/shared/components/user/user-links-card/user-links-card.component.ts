import {Component, Input} from '@angular/core';
import {CommonModule} from '@angular/common';
import {UserModel} from 'src/app/core/models/user.model';

@Component({
  selector: 'app-user-links-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-links-card.component.html',
  styleUrls: ['./user-links-card.component.css']
})
export class UserLinksCardComponent {
  @Input() user: UserModel | null = null;
}
