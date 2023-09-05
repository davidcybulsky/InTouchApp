import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FriendModel } from 'src/app/core/models/friend.model';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-friend-card',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './friend-card.component.html',
  styleUrls: ['./friend-card.component.css']
})
export class FriendCardComponent {
  @Input() friend : FriendModel | null = null;
}
