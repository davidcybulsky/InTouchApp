import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { UserLinksCardComponent } from 'src/app/shared/components/user/user-links-card/user-links-card.component';
import { UserPanelComponent } from './user-panel/user-panel.component';
import { HeaderComponent } from 'src/app/shared/components/header/header.component';
import { FooterComponent } from 'src/app/shared/components/footer/footer.component';
import { PostCardComponent } from 'src/app/shared/components/post/post-card/post-card.component';
import { Observable, of } from 'rxjs';
import { PostModel } from 'src/app/core/models/post.model';
import { PostService } from 'src/app/core/services/post.service';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    UserLinksCardComponent,
    UserPanelComponent,
    HeaderComponent,
    FooterComponent,
    PostCardComponent
  ],
  selector: 'app-userpage',
  templateUrl: './userpage.component.html',
  styleUrls: ['./userpage.component.css']
})
export class UserpageComponent implements OnInit{

  posts$: Observable<PostModel[]> = of([]);  

  constructor(private postService: PostService) { }

  ngOnInit(): void {
    let id = 0;
    this.posts$ = this.postService.getUserPosts(id);
  }

}
