import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import { IEnvoronment } from 'src/environment/environment.interface';
import { PostServiceEndpoints } from '../enums/post.service.endpoints';
import { PostModel } from '../models/post.model';
import { CreateQuickPostModel } from '../models/create.quick.post.model';
import { CreatePostModel } from '../models/create.post.model';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private http: HttpClient) { }

  getPosts() {
    return this.http.get<PostModel[]>(`${ this.ENVIRONMENT_TOKEN.serverEndpoint }${ PostServiceEndpoints.GET_POSTS}` )
  }

  createPost(model: CreatePostModel) {
    return this.http.post<number>(`${ this.ENVIRONMENT_TOKEN.serverEndpoint }${ PostServiceEndpoints.CREATE_POST}`, model)
  }

  createQuickPost(model: CreateQuickPostModel) {
    return this.http.post<number>(`${ this.ENVIRONMENT_TOKEN.serverEndpoint }${ PostServiceEndpoints.CREATE_POST}`, model)
  }
}
