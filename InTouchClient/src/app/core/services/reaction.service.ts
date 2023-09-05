import { Inject, Injectable } from '@angular/core';
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import { IEnvoronment } from 'src/environment/environment.interface';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ReactionService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private httpClient: HttpClient) { }
              
  addPostLike(postId: number) {
  }

  addPostDisLike(postId: number) {
  }
}
