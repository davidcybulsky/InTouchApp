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
export class MessageCardComponent implements OnInit{
  @Input() message: MessageModel | null = null

  ngOnInit(): void {
    console.log(this.message)
  }
}
