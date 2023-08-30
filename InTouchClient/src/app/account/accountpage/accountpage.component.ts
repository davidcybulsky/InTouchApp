import { AsyncPipe, CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AccountModel } from 'src/app/core/models/account.model';
import { FriendModel } from 'src/app/core/models/friend.model';
import { AccountService } from 'src/app/core/services/account.service';
import { FriendService } from 'src/app/core/services/friend.service';
import { FooterComponent } from 'src/app/shared/components/footer/footer.component';
import { FriendCardComponent } from 'src/app/shared/components/friend/friend-card/friend-card.component';
import { FriendRequestCardComponent } from 'src/app/shared/components/friend/friend-request-card/friend-request-card.component';
import { HeaderComponent } from 'src/app/shared/components/header/header.component';
import { CreateQuickPostComponent } from 'src/app/shared/components/post/create-quick-post/create-quick-post.component';
import { PostCardComponent } from 'src/app/shared/components/post/post-card/post-card.component';
import { UserLinksCardComponent } from 'src/app/shared/components/user/user-links-card/user-links-card.component';
import { AccountPanelComponent } from './account-panel/account-panel.component';
import { Router, RouterModule } from '@angular/router';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    HeaderComponent,
    FooterComponent,
    FriendCardComponent,
    FriendRequestCardComponent,
    CreateQuickPostComponent,
    PostCardComponent,
    UserLinksCardComponent,
    AccountPanelComponent,
    AsyncPipe
  ],
  selector: 'app-accountpage',
  templateUrl: './accountpage.component.html',
  styleUrls: ['./accountpage.component.css']
})
export class AccountpageComponent implements OnInit{  
  constructor(private accountService: AccountService,
              private friendService: FriendService,
              private router: Router) { }
  
  account$: Observable<AccountModel | null> = of(null);
  friends$: Observable<FriendModel[]> = of([]);
  friendRequests$: Observable<FriendModel[]> = of([]);

  ngOnInit(): void {
    this.account$ = this.accountService.getAccount()
    this.friends$ = this.friendService.getFriends()
    this.friendRequests$ = this.friendService.getFriendRequests()
  }

  onEditAccount() {
    this.router.navigate(['account','edit']);
  }
}
