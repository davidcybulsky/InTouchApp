import {CommonModule} from '@angular/common';
import {Component, OnDestroy, OnInit} from '@angular/core';
import {ReplaySubject, takeUntil} from 'rxjs';
import {AccountModel} from 'src/app/core/models/account.model';
import {FriendModel} from 'src/app/core/models/friend.model';
import {AccountService} from 'src/app/core/services/account.service';
import {FriendService} from 'src/app/core/services/friend.service';
import {FooterComponent} from 'src/app/shared/components/footer/footer.component';
import {HeaderComponent} from 'src/app/shared/components/header/header.component';
import {CreateQuickPostComponent} from 'src/app/shared/components/post/create-quick-post/create-quick-post.component';
import {UserLinksCardComponent} from 'src/app/shared/components/user/user-links-card/user-links-card.component';
import {AccountPanelComponent} from './account-panel/account-panel.component';
import {PostPageComponent} from 'src/app/shared/components/post/post-page/post-page.component';
import {FriendCardPanelComponent} from 'src/app/shared/components/friend/friend-card-panel/friend-card-panel.component';
import {
  FriendRequestsPanelComponent
} from 'src/app/shared/components/friend/friend-requests-panel/friend-requests-panel.component';
import {PostService} from "../../core/services/post.service";
import {CreatePostModel} from "../../core/models/create.post.model";

@Component({
  standalone: true,
  imports: [
    CommonModule,
    HeaderComponent,
    FooterComponent,
    CreateQuickPostComponent,
    UserLinksCardComponent,
    AccountPanelComponent,
    PostPageComponent,
    FriendCardPanelComponent,
    FriendRequestsPanelComponent
  ],
  selector: 'app-accountpage',
  templateUrl: './accountpage.component.html',
  styleUrls: ['./accountpage.component.css']
})
export class AccountpageComponent implements OnInit, OnDestroy {

  destroy$: ReplaySubject<boolean> = new ReplaySubject(1);

  account: AccountModel | null = null
  friends: FriendModel[] = []
  friendRequests: FriendModel[] = []

  constructor(private accountService: AccountService,
              private friendService: FriendService,
              private postService: PostService) {
  }

  ngOnInit(): void {
    this.accountService.getAccount()
      .pipe(takeUntil(this.destroy$))
      .subscribe(account => {
        this.account = account
      })
    this.friendService.getFriends()
      .pipe(takeUntil(this.destroy$))
      .subscribe(friends => {
        this.friends = friends
      })
    this.friendService.getFriendRequests()
      .pipe(takeUntil(this.destroy$))
      .subscribe(friendRequests => {
        this.friendRequests = friendRequests
      })
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }

  onCreatePost(model: CreatePostModel) {
    this.postService.createPost(model).subscribe(response => {
      this.account?.posts.unshift(response)
    })
  }

}
