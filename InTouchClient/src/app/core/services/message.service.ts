import {Inject, Injectable} from '@angular/core';
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {IEnvoronment} from "../../../environment/environment.interface";
import {HubConstants} from "../enums/hub.constants";
import {TokenModel} from "../models/token.model";
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import {MessageHubMethods} from "../enums/message.hub.methods";
import {MessageModel} from "../models/message.model";
import {BehaviorSubject, take} from "rxjs";
import {error} from "@angular/compiler-cli/src/transformers/util";
import {SendMessageModel} from "../models/send.message.model";

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private hubConnection?: HubConnection
  messages: BehaviorSubject<MessageModel []> = new BehaviorSubject([] as MessageModel[]);
  messages$ = this.messages.asObservable();
  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment) { }

  createHubConnection(tokenData: TokenModel, friendId: number) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.ENVIRONMENT_TOKEN.hubUrl}${HubConstants.MESSAGE_HUB}${HubConstants.USER_PARAM}${friendId}`,
        {accessTokenFactory: () => tokenData.token.slice(7)
          })
      .withAutomaticReconnect()
      .build()

    this.hubConnection.start().catch(error => console.log(error))

    this.hubConnection.on(MessageHubMethods.GET_MESSAGE_THREAD, (messages: MessageModel[]) => {
      this.messages.next(messages);
    })

    this.hubConnection.on(MessageHubMethods.NEW_MESSAGE, (message: MessageModel) => {
      this.messages$.pipe(take(1)).subscribe({
        next: messages => {
          this.messages.next([message, ...messages])
        }
        })
    })
  }

  stopHubConnection() {
    this.hubConnection?.stop().catch(error => console.log(error))
  }

  SendMessage(recipientId: number, content: string) {
    const message: SendMessageModel = {
      recipientId: recipientId,
      content: content
    }

    console.log(message)

    return this.hubConnection?.invoke(MessageHubMethods.SEND_MESSAGE,message)
      .catch(error => console.log(error))
  }
}
