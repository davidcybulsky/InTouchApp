import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FriendModel } from '../models/friend.model';
import { HttpClient } from '@angular/common/http';
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import { IEnvoronment } from 'src/environment/environment.interface';
import { FriendServiceEndpoints } from '../enums/friend.service.endpoints';

@Injectable({
  providedIn: 'root'
})
export class FriendService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private httpClient: HttpClient) {}

  getFriendRequests(): Observable<FriendModel[]> {
    return this.httpClient.get<FriendModel[]>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${FriendServiceEndpoints.GET_FRIEND_REQUESTS}`)
  }

  getFriends(): Observable<FriendModel[]> {
    return this.httpClient.get<FriendModel[]>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${FriendServiceEndpoints.GET_FRINEDS}`)
  }

  getUserFriendRequests(userId: number): Observable<FriendModel[]> {
    throw new Error('Method not implemented.');
  }

  getUserFriends(userId: number): Observable<FriendModel[]> {
    return this.httpClient.get<FriendModel[]>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${FriendServiceEndpoints.GET_USER_FRIENDS}/${userId}`)
  }

  acceptFriendRequest(friendId: number): Observable<void> {
    return this.httpClient.put<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${FriendServiceEndpoints.ACCEPT_FRIEND_REQUEST}/${friendId}`,null)
  }

  sendFriendRequest(friendId: number): Observable<void> {
    return this.httpClient.post<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${FriendServiceEndpoints.SEND_FRIEND_REQUEST}/${friendId}`,null)
  }
}
