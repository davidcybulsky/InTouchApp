import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserModel } from '../models/user.model';
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import { IEnvoronment } from 'src/environment/environment.interface';
import { UserServiceEndpoints } from '../enums/user.service.endpoints';
import { CreateUserModel } from '../models/create.user.model';
import { UpdateUserModel } from '../models/update.user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private httpClient: HttpClient) { }

  getUserById(userId: number): Observable<UserModel> {
    return this.httpClient.get<UserModel>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${UserServiceEndpoints.GET_USER_BY_ID}/${userId}`)
  }

  getUsers(): Observable<UserModel[]> {
    return this.httpClient.get<UserModel[]>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${UserServiceEndpoints.GET_USERS}`)
  }

  createUser(model: CreateUserModel): Observable<number> {
    return this.httpClient.post<number>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${UserServiceEndpoints.CREATE_USER}`,model)
  }

  updateUser(userId: number, model: UpdateUserModel): Observable<void> {
    return this.httpClient.put<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${UserServiceEndpoints.UPDATE_USER}${userId}`, model)
  }

  deleteUser(userId: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${UserServiceEndpoints.DELETE_USER}/${userId}`)
  }
}