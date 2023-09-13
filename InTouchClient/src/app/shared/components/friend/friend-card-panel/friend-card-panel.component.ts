import {Component, Input} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FriendModel} from 'src/app/core/models/friend.model';
import {FriendCardComponent} from './friend-card/friend-card.component';

@Component({
  selector: 'app-friend-card-panel',
  standalone: true,
  imports: [
    CommonModule,
    FriendCardComponent
  ],
  templateUrl: './friend-card-panel.component.html',
  styleUrls: ['./friend-card-panel.component.css']
})
export class FriendCardPanelComponent {

  @Input() friends: FriendModel[] = [];

}
