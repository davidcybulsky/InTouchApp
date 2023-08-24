import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserModel } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  getUserById(): Observable<UserModel> {
    throw new Error('Method not implemented.');
  }

  getUsers(): Observable<UserModel[]> {
    throw new Error('Method not implemented.');
  }

  createUser(): Observable<number> {
    throw new Error('Method not implemented.');
  }

  updateUser(): Observable<void> {
    throw new Error('Method not implemented.');
  }

  deleteUser(): Observable<void> {
    throw new Error('Method not implemented.');
  }
}