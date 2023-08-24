import { Component, OnDestroy, OnInit } from '@angular/core';
import { HeaderComponent } from '../shared/components/header/header.component';
import { FooterComponent } from '../shared/components/footer/footer.component';
import { PostService } from '../core/services/post.service';
import { Observable, of } from 'rxjs';
import { PostModel } from '../core/models/post.model';
import { PostCardComponent } from '../shared/components/post/post-card/post-card.component';
import { CommonModule } from '@angular/common';
import { CreateQuickPostComponent } from '../shared/components/post/create-quick-post/create-quick-post.component';
import { FriendCardComponent } from '../shared/components/friend/friend-card/friend-card.component';
import { FriendModel } from '../core/models/friend.model';
import { FriendService } from '../core/services/friend.service';
import { FriendRequestCardComponent } from '../shared/components/friend/friend-request-card/friend-request-card.component';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    HeaderComponent,
    FooterComponent,
    PostCardComponent,
    CreateQuickPostComponent,
    FriendCardComponent,
    FriendRequestCardComponent
  ],
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {
  
  constructor(private postService: PostService, private friendService: FriendService) { }
  
  posts$: Observable<PostModel[]> = of([]);
  friends$: Observable<FriendModel[]> = of([]);
  friendRequests$: Observable<FriendModel[]> = of([]);

  ngOnInit(): void {
    this.posts$ = this.postService.getPosts()
    this.friends$ = this.friendService.getFriends()
    this.friendRequests$ = this.friendService.getFriendRequests()
  }
}
