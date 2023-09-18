import {HttpClient} from '@angular/common/http';
import {Inject, Injectable} from '@angular/core';
import {IEnvoronment} from 'src/environment/environment.interface';
import {PostServiceEndpoints} from '../enums/post.service.endpoints';
import {PostModel} from '../models/post.model';
import {CreateQuickPostModel} from '../models/create.quick.post.model';
import {CreatePostModel} from '../models/create.post.model';
import {Observable} from 'rxjs';
import {UpdatePostModel} from '../models/update.post.model';
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private http: HttpClient) {
  }

  getPosts(): Observable<PostModel[]> {
    return this.http.get<PostModel[]>(`${this.ENVIRONMENT_TOKEN.apiUrl}${PostServiceEndpoints.GET_POSTS}`)
  }

  getUserPosts(userId: number): Observable<PostModel[]> {
    return this.http.get<PostModel[]>(`${this.ENVIRONMENT_TOKEN.apiUrl}${PostServiceEndpoints.GET_USER_POSTS}${userId}`);
  }

  createPost(model: CreatePostModel): Observable<PostModel> {
    return this.http.post<PostModel>(`${this.ENVIRONMENT_TOKEN.apiUrl}${PostServiceEndpoints.CREATE_POST}`, model)
  }

  createQuickPost(model: CreateQuickPostModel): Observable<PostModel> {
    return this.http.post<PostModel>(`${this.ENVIRONMENT_TOKEN.apiUrl}${PostServiceEndpoints.CREATE_POST}`, model)
  }

  updatePost(postId: number, model: UpdatePostModel): Observable<void> {
    return this.http.put<void>(`${this.ENVIRONMENT_TOKEN.apiUrl}${PostServiceEndpoints.UPDATE_POST}${postId}`, model)
  }

  deletePost(postId: number) {
    return this.http.delete<void>(`${this.ENVIRONMENT_TOKEN.apiUrl}${PostServiceEndpoints.DELETE_POST}${postId}`)
  }
}
