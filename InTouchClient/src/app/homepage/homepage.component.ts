import { Component, OnDestroy, OnInit } from '@angular/core';
import { HeaderComponent } from '../shared/components/header/header.component';
import { FooterComponent } from '../shared/components/footer/footer.component';
import { PostService } from '../core/services/post.service';
import { Observable, of } from 'rxjs';
import { PostModel } from '../core/models/post.model';
import { PostCardComponent } from '../shared/components/post/post-card/post-card.component';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    HeaderComponent,
    FooterComponent,
    PostCardComponent
  ],
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {
  
  constructor(private postService: PostService) { }
  
  posts$ : Observable<PostModel[]> = of([]);

  ngOnInit(): void {
    this.posts$ = this.postService.getPosts()
  }
}
