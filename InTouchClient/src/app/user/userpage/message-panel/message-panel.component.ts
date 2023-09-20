import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {CommonModule} from "@angular/common";
import {MessageService} from "../../../core/services/message.service";
import {MessageModel} from "../../../core/models/message.model";
import {Observable, of, ReplaySubject, takeUntil} from "rxjs";
import {MessageCardComponent} from "./message-card/message-card.component";
import {AuthService} from "../../../core/services/auth.service";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";

@Component({
  standalone: true,
  imports: [
    CommonModule,
    MessageCardComponent,
    ReactiveFormsModule
  ],
  selector: 'app-message-panel',
  templateUrl: './message-panel.component.html',
  styleUrls: ['./message-panel.component.css']
})
export class MessagePanelComponent implements OnInit, OnDestroy{
  messages : MessageModel[] = []
  @Input() userId: number | null = null
  messageForm!: FormGroup
  destroy$ = new ReplaySubject<boolean>(1)
  numberOfMessages: number = 5;

  constructor(private messageService: MessageService,
              private authService: AuthService,
              private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    let authData = this.authService.getJWTTokenData()
    if(authData && this.userId) {
      this.messageService.createHubConnection(authData,this.userId)
    }
    this.messageForm = this.formBuilder.group({
      content: ['', Validators.required]
    })
    this.messageService.messages.pipe(takeUntil(this.destroy$)).subscribe(
      response => {
        this.messages = response
        console.log(response)
      }
    )
  }

  onSendMessage() {
    if(this.userId) {
      this.messageService.SendMessage(this.userId,this.messageForm.get("content")?.value)?.then( value =>
      {
        this.numberOfMessages = this.numberOfMessages + 1
        this.messageForm.reset()
      })
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next(true)
    this.destroy$.complete()
  }
}
