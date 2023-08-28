import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import { IEnvoronment } from 'src/environment/environment.interface';
import { PostServiceEndpoints } from '../enums/post.service.endpoints';
import { PostModel } from '../models/post.model';
import { CreateQuickPostModel } from '../models/create.quick.post.model';
import { CreatePostModel } from '../models/create.post.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private http: HttpClient) { }

  getPosts(): Observable<PostModel[]> {
    return this.http.get<PostModel[]>(`${ this.ENVIRONMENT_TOKEN.serverEndpoint }${ PostServiceEndpoints.GET_POSTS}` )
  }

  getUserPosts(id: number): Observable<PostModel[]> {
    throw new Error('Method not implemented.');
  }

  createPost(model: CreatePostModel): Observable<number> {
    return this.http.post<number>(`${ this.ENVIRONMENT_TOKEN.serverEndpoint }${ PostServiceEndpoints.CREATE_POST}`, model)
  }

  createQuickPost(model: CreateQuickPostModel): Observable<number> {
    return this.http.post<number>(`${ this.ENVIRONMENT_TOKEN.serverEndpoint }${ PostServiceEndpoints.CREATE_POST}`, model)
  }

  updatePost() {
    throw new Error('Method not implemented.');
  }

  deletePost() {
    throw new Error('Method not implemented.');
  }
}