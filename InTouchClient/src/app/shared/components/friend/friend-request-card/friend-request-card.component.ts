import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FriendModel } from 'src/app/core/models/friend.model';

@Component({
  selector: 'app-friend-request-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './friend-request-card.component.html',
  styleUrls: ['./friend-request-card.component.css']
})
export class FriendRequestCardComponent {
  @Input() friendRequest: FriendModel | null = null;
}
