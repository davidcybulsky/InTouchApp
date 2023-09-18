import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {UserModel} from "../models/user.model";
import {Observable} from "rxjs";
import {ENVIRONMENT_TOKEN} from "../tokens/environment.token";
import {IEnvoronment} from "../../../environment/environment.interface";
import {SearchServiceEndpoints} from "../enums/search.service.endpoints";
import {PostModel} from "../models/post.model";

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private httpClient: HttpClient) { }

  getUsers(pattern: string): Observable<UserModel[]>  {
    return this.httpClient.get<UserModel[]>(`${this.ENVIRONMENT_TOKEN.apiUrl}${SearchServiceEndpoints.GET_USERS}${pattern}`)
  }

  getPosts(pattern: string): Observable<PostModel[]>  {
    return this.httpClient.get<PostModel[]>(`${this.ENVIRONMENT_TOKEN.apiUrl}${SearchServiceEndpoints.GET_POSTS}${pattern}`)
  }
}
