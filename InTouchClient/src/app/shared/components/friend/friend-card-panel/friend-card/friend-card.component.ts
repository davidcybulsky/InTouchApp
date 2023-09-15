import {Component, Input} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FriendModel} from 'src/app/core/models/friend.model';
import {RouterModule} from '@angular/router';
import {faMessage} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";

@Component({
  selector: 'app-friend-card',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FontAwesomeModule
  ],
  templateUrl: './friend-card.component.html',
  styleUrls: ['./friend-card.component.css']
})
export class FriendCardComponent {

  @Input() friend: FriendModel | null = null
  message = faMessage
  openMessageWindow: boolean = false

  onCloseMessageWindow() {

  }
}
