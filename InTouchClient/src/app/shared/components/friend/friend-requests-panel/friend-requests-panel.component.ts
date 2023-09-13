import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FriendModel} from 'src/app/core/models/friend.model';
import {FriendRequestCardComponent} from './friend-request-card/friend-request-card.component';

@Component({
  selector: 'app-friend-requests-panel',
  standalone: true,
  imports: [
    CommonModule,
    FriendRequestCardComponent
  ],
  templateUrl: './friend-requests-panel.component.html',
  styleUrls: ['./friend-requests-panel.component.css']
})
export class FriendRequestsPanelComponent {

  @Input() friendRequests: FriendModel[] = []
  @Output() acceptFriendRequest: EventEmitter<number> = new EventEmitter()

  onAcceptFriendRequest(id: number) {
    this.acceptFriendRequest.emit(id)
  }

}
