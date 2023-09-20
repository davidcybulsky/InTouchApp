import {Component, Input, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MessageModel} from "../../../../core/models/message.model";

@Component({
  selector: 'app-message-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './message-card.component.html',
  styleUrls: ['./message-card.component.css']
})
export class MessageCardComponent{
  @Input() message: MessageModel | null = null

  get mainPhotoUrl(): string | undefined {
    if (this.message && this.message.sender) {
      const mainPhoto = this.message.sender.userPhoto;
      return mainPhoto ? mainPhoto.url : undefined;
    }
    return undefined;
  }
}
