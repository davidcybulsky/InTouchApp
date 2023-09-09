import { Inject, Injectable } from '@angular/core';
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import { IEnvoronment } from 'src/environment/environment.interface';
import { HttpClient } from '@angular/common/http';
import { CreateReactionModel } from '../models/create.reaction.model';
import { ReactionConstants } from '../enums/reaction.constants';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReactionService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private httpClient: HttpClient) { }
              
  addPostLike(postId: number) : Observable<void> {
    const reactionModel: CreateReactionModel = {
      reactionType: ReactionConstants.LIKE
    }
    return this.httpClient.post<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}`, null)
  }

  addPostDisLike(postId: number) {
    const reactionModel: CreateReactionModel = {
      reactionType: ReactionConstants.DISLIKE
    }
    return this.httpClient.post<void>(`${ this.ENVIRONMENT_TOKEN.serverEndpoint}`, null)
  }
}
