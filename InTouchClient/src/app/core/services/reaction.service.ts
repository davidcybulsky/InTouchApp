import {Inject, Injectable} from '@angular/core';
import {IEnvoronment} from 'src/environment/environment.interface';
import {HttpClient} from '@angular/common/http';
import {CreateReactionModel} from '../models/create.reaction.model';
import {ReactionConstants} from '../enums/reaction.constants';
import {Observable} from 'rxjs';
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import {IncludeReactionModel} from "../models/include.reaction.model";
import {ReactionServiceEndpoints} from "../enums/reaction.service.endpoints";
import {UpdateReactionModel} from "../models/update.reaction.model";

@Injectable({
  providedIn: 'root'
})
export class ReactionService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private httpClient: HttpClient) {
  }

  addPostReaction(postId: number, reactionType: string) : Observable<void> {
    const reactionModel: CreateReactionModel = {
      reactionType: reactionType
    }
    return this.httpClient.post<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${ReactionServiceEndpoints.CREATE_POST_REACTION}${postId}`, reactionModel)
  }

  addCommentReaction(commentId: number, reactionType: string) : Observable<void> {
    const reactionModel: CreateReactionModel = {
      reactionType: reactionType
    }
    return this.httpClient.post<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${ReactionServiceEndpoints.CREATE_COMMENT_REACTION}${commentId}`, reactionModel)
  }

  updatePostReaction(postId: number, reactionType: string) : Observable<void> {
    const reactionModel: UpdateReactionModel = {
      reactionType: reactionType
    }
    return this.httpClient.put<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${ReactionServiceEndpoints.UPDATE_POST_REACTION}${postId}`, reactionModel)
  }

  updateCommentReaction(commentId: number, reactionType: string) : Observable<void> {
    const reactionModel: UpdateReactionModel = {
      reactionType: reactionType
    }
    return this.httpClient.put<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${ReactionServiceEndpoints.UPDATE_COMMENT_REACTION}${commentId}`,reactionModel)
  }

  deletePostReaction(postId: number) : Observable<void> {
    return this.httpClient.delete<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${ReactionServiceEndpoints.DELETE_POST_REACTION}${postId}`)
  }

  deleteCommentReaction(commentId: number) : Observable<void> {
    return this.httpClient.delete<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${ReactionServiceEndpoints.DELETE_COMMENT_REACTION}${commentId}`)
  }

}
