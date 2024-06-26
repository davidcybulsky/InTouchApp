import {Component, OnDestroy, OnInit} from '@angular/core';
import {HeaderComponent} from '../shared/components/header/header.component';
import {FooterComponent} from '../shared/components/footer/footer.component';
import {PostService} from '../core/services/post.service';
import {ReplaySubject, takeUntil} from 'rxjs';
import {PostModel} from '../core/models/post.model';
import {CommonModule} from '@angular/common';
import {CreateQuickPostComponent} from '../shared/components/post/create-quick-post/create-quick-post.component';
import {FriendModel} from '../core/models/friend.model';
import {FriendService} from '../core/services/friend.service';
import {FriendCardPanelComponent} from '../shared/components/friend/friend-card-panel/friend-card-panel.component';
import {
  FriendRequestsPanelComponent
} from '../shared/components/friend/friend-requests-panel/friend-requests-panel.component';
import {PostPageComponent} from '../shared/components/post/post-page/post-page.component';
import {CreatePostModel} from '../core/models/create.post.model';
import {ConnectionService} from "../core/services/connection.service";

@Component({
  standalone: true,
  imports: [
    CommonModule,
    HeaderComponent,
    FooterComponent,
    CreateQuickPostComponent,
    FriendCardPanelComponent,
    FriendRequestsPanelComponent,
    PostPageComponent
  ],
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit, OnDestroy {

  posts: PostModel[] = []
  friends: FriendModel[] = []
  friendRequests: FriendModel[] = []
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1)

  constructor(private postService: PostService,
              private friendService: FriendService,
              private connectionService: ConnectionService) {
  }

  ngOnInit(): void {

    this.postService.getPosts()
      .pipe(takeUntil(this.destroyed$))
      .subscribe(posts => {
        this.posts = posts
      })

    this.friendService.getFriends()
      .pipe(takeUntil(this.destroyed$))
      .subscribe(friends => {
        this.friends = friends
      })

    this.friendService.getFriendRequests()
      .pipe(takeUntil(this.destroyed$))
      .subscribe(friendRequests => {
        this.friendRequests = friendRequests
      })
  }

  ngOnDestroy(): void {
    this.destroyed$.next(true)
    this.destroyed$.complete()
  }

  onCreateQuickPost(model: CreatePostModel) {
    this.postService.createPost(model).subscribe(response => {
      this.posts.unshift(response)
    })
  }

  onAcceptFriendRequest(id: number) {
    this.friendService.acceptFriendRequest(id).subscribe()
  }
}
