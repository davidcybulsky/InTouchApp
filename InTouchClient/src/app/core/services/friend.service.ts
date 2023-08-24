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
}
