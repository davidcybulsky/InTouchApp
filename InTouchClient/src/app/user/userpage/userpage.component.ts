import {CommonModule} from '@angular/common';
import {Component, OnDestroy, OnInit} from '@angular/core';
import {UserLinksCardComponent} from 'src/app/shared/components/user/user-links-card/user-links-card.component';
import {UserPanelComponent} from './user-panel/user-panel.component';
import {HeaderComponent} from 'src/app/shared/components/header/header.component';
import {FooterComponent} from 'src/app/shared/components/footer/footer.component';
import {Observable, of, ReplaySubject, takeUntil} from 'rxjs';
import {PostModel} from 'src/app/core/models/post.model';
import {PostService} from 'src/app/core/services/post.service';
import {ActivatedRoute, Params} from '@angular/router';
import {UserModel} from 'src/app/core/models/user.model';
import {UserService} from 'src/app/core/services/user.service';
import {FriendService} from 'src/app/core/services/friend.service';
import {FriendModel} from 'src/app/core/models/friend.model';
import {PostPageComponent} from 'src/app/shared/components/post/post-page/post-page.component';
import {FriendCardPanelComponent} from 'src/app/shared/components/friend/friend-card-panel/friend-card-panel.component';
import {MessageService} from "../../core/services/message.service";
import {MessagePanelComponent} from "./message-panel/message-panel.component";
import {AuthService} from "../../core/services/auth.service";
import {MessageModel} from "../../core/models/message.model";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  standalone: true,
  imports: [
    CommonModule,
    UserLinksCardComponent,
    UserPanelComponent,
    HeaderComponent,
    FooterComponent,
    PostPageComponent,
    FriendCardPanelComponent,
    MessagePanelComponent
  ],
  selector: 'app-userpage',
  templateUrl: './userpage.component.html',
  styleUrls: ['./userpage.component.css']
})
export class UserpageComponent implements OnInit, OnDestroy {

  destroy$: ReplaySubject<boolean> = new ReplaySubject(1);

  userId: number | null = null
  user: UserModel | null = null
  posts: PostModel[] = []
  friends: FriendModel[] = []
  messages$: Observable<MessageModel[]> = of([])

  constructor(private postService: PostService,
              private userService: UserService,
              private friendService: FriendService,
              private route: ActivatedRoute,
              private messageService: MessageService,
              private authService: AuthService,
              private fb: FormBuilder) {
  }

  ngOnInit(): void {
    this.route.params
      .subscribe((params: Params) => {
        this.userId = params['id'];
        if (this.userId) {
          this.userService.getUserById(this.userId)
            .pipe(takeUntil(this.destroy$))
            .subscribe(user => {
              this.user = user
            })
          this.postService.getUserPosts(this.userId)
            .pipe(takeUntil(this.destroy$))
            .subscribe(posts => {
              this.posts = posts
            })
          this.friendService.getUserFriends(this.userId)
            .pipe(takeUntil(this.destroy$))
            .subscribe(friends => {
              this.friends = friends
            })
        }
      })
    let token = this.authService.getJWTTokenData()
    if(token && this.userId)
    {
      this.messageService.createHubConnection(token, this.userId)
      this.messages$ = this.messageService.messages$
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next(true)
    this.destroy$.complete()
  }

  onSendMessage(content: string) {
    if(this.userId)
      this.messageService.SendMessage(this.userId,content)?.then(() => {
      })
  }
}
