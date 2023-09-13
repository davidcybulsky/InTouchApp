import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FriendModel} from 'src/app/core/models/friend.model';
import {RouterModule} from '@angular/router';

@Component({
  selector: 'app-friend-request-card',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './friend-request-card.component.html',
  styleUrls: ['./friend-request-card.component.css']
})
export class FriendRequestCardComponent {

  @Input() friendRequest: FriendModel | null = null
  @Output() acceptFriendRequest: EventEmitter<number> = new EventEmitter()

  onAcceptFriendRequest() {
    this.acceptFriendRequest.emit(this.friendRequest?.id)
  }
}
