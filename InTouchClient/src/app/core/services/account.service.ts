import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountModel } from '../models/account.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private httpClient: HttpClient) { }

  getAccount(): Observable<AccountModel> {
    throw new Error('Method not implemented.');
  }

  updateAccount(): Observable<void> {
    throw new Error('Method not implemented.');
  }

  deleteAccount(): Observable<void> {
    throw new Error('Method not implemented.');
  }
}
