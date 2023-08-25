import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { AccountModel } from '../models/account.model';
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import { IEnvoronment } from 'src/environment/environment.interface';
import { AccountServiceEndpoint } from '../enums/account.service.endpoints';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private httpClient: HttpClient) { }

  getAccount(): Observable<AccountModel> {
    return this.httpClient.get<AccountModel>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${AccountServiceEndpoint.GET_ACCOUNT}`)
    .pipe(map(response => {
      console.log(response)
      return response}));
  }

  updateAccount(model: any): Observable<void> {
    return this.httpClient.put<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${AccountServiceEndpoint.UPDATE_ACCOUNT}`, model);
  }

  deleteAccount(): Observable<void> {
    return this.httpClient.delete<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${AccountServiceEndpoint.DELETE_ACCOUNT}`);
  }
}
