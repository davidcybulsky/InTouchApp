import { Inject, Injectable } from '@angular/core';
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import { IEnvoronment } from 'src/environment/environment.interface';
import { HttpClient } from '@angular/common/http';
import { UpdateCommentModel } from '../models/update.comment.model';
import { CreateCommentModel } from './create.comment.model';
import { Observable } from 'rxjs';
import { CommentServiceEndpoints } from '../enums/comment.service.endpoints';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private httpClient: HttpClient) { }
  
  createPostComment(postId: number, model: CreateCommentModel): Observable<number> {
    return this.httpClient.post<number>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${CommentServiceEndpoints.CREATE_POST_COMMENT}/${postId}`, model)
  }

  updatePostComment(commentId: number, model: UpdateCommentModel): Observable<void> {
    return this.httpClient.put<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${CommentServiceEndpoints.UPDATE_POST_COMMENT}/${commentId}`,model)
  }

  deletePostComment(commentId: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${CommentServiceEndpoints.DELETE_POST_COMMENT}/${commentId}`)
  }
}