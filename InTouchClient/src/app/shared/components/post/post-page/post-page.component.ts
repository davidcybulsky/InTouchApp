import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { PostModel } from 'src/app/core/models/post.model';
import { PostCardComponent } from './post-card/post-card.component';

@Component({
  standalone: true,
  selector: 'app-post-page',
  imports: [
    CommonModule,
    PostCardComponent
  ],
  templateUrl: './post-page.component.html',
  styleUrls: ['./post-page.component.css']
})
export class PostPageComponent {

  @Input() posts: PostModel[] | undefined = [];

}
