import {Inject, Injectable} from '@angular/core';
import {IEnvoronment} from "../../../environment/environment.interface";
import {HttpTransportType, HubConnection, HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import {HubConstants} from "../enums/hub.constants";
import {TokenModel} from "../models/token.model";
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import {ConnectionHubMethods} from "../enums/connection.hub.methods";

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {

  private hubConnection?: HubConnection
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

    this.hubConnection.on(ConnectionHubMethods.USER_IS_ONLINE, message => {
      console.log(message)
    })

    this.hubConnection.on(ConnectionHubMethods.USER_IS_OFFLINE, message => {
      console.log(message)
    })
  }

  stopHubConnection() {
    this.hubConnection?.stop().catch(error => console.log(error))
  }
}
