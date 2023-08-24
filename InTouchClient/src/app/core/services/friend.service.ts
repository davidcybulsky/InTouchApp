import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FriendModel } from '../models/friend.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FriendService {

  constructor(private httpClient: HttpClient) {}

  getFriendRequests(): Observable<FriendModel[]> {
    throw new Error('Method not implemented.');
  }

  getFriends(): Observable<FriendModel[]> {
    throw new Error('Method not implemented.');
  }

  getUserFriendRequests(userId: number): Observable<FriendModel[]> {
    throw new Error('Method not implemented.');
  }

  getUserFriends(userId: number): Observable<FriendModel[]> {
    throw new Error('Method not implemented.');
  }

  acceptFriendRequest(friendId: number): Observable<void> {
    throw new Error('Method not implemented.');
  }

  sendFriendRequest(friendId: number): Observable<void> {
    throw new Error('Method not implemented.');
  }

}
