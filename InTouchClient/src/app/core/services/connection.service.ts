import {Inject, Injectable} from '@angular/core';
import {IEnvoronment} from "../../../environment/environment.interface";
import {HubConnection, HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import {HubConstants} from "../enums/hub.constants";
import {TokenModel} from "../models/token.model";
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import {ConnectionHubMethods} from "../enums/connection.hub.methods";
import {BehaviorSubject, of} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {

  private hubConnection?: HubConnection

  connected: string[] = []

  connectedUserIds = new BehaviorSubject<string[]>(this.connected);

  connectedUserIds$ = this.connectedUserIds.asObservable()

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment) { }

  createHubConnection(tokenData: TokenModel) {
    console.log(tokenData.token)
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.ENVIRONMENT_TOKEN.hubUrl}${HubConstants.CONNECTION_HUB}`, {
        accessTokenFactory: () => tokenData.token.slice(7)
      })
      .configureLogging(LogLevel.Information)
      .withAutomaticReconnect()
      .build()

    this.hubConnection.start().catch(error => console.log(error))

    this.hubConnection.on(ConnectionHubMethods.FRIEND_IS_ONLINE, (id : string) => {
      if(!this.connected.includes(id)) {
        this.connected.push(id)
        console.log(this.connected)
        this.connectedUserIds.next(this.connected)
      }
    })

    this.hubConnection.on(ConnectionHubMethods.FRIEND_IS_OFFLINE, (id : string) => {
      if(this.connected.includes(id)) {
        this.connected = this.connected.filter(c => c != id)
        console.log(this.connected)
        this.connectedUserIds.next(this.connected)
      }
    })
  }

  stopHubConnection() {
    this.hubConnection?.stop().catch(error => console.log(error))
  }
}
