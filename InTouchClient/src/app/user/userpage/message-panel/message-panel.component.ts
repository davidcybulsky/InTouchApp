import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {CommonModule} from "@angular/common";
import {MessageService} from "../../../core/services/message.service";
import {MessageModel} from "../../../core/models/message.model";
import {Observable, of} from "rxjs";
import {MessageCardComponent} from "./message-card/message-card.component";

@Component({
  standalone: true,
  imports: [
    CommonModule,
    MessageCardComponent
  ],
  selector: 'app-message-panel',
  templateUrl: './message-panel.component.html',
  styleUrls: ['./message-panel.component.css']
})
export class MessagePanelComponent {
  @Input() messages$: Observable<MessageModel[]> = of([])
  @Output() SendMessage = new EventEmitter<string>();

  onSendMessage(content: string) {
    this.SendMessage.emit(content)
  }
}
