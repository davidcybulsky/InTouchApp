import {CommonModule} from '@angular/common';
import {Component, EventEmitter, Input, Output} from '@angular/core';
import {PostModel} from 'src/app/core/models/post.model';
import {PostCardComponent} from './post-card/post-card.component';
import {CreateCommentModel} from 'src/app/core/models/create.comment.model';

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

    onDeletePost(postId: number) {
        this.posts = this.posts?.filter(p => p.id != postId)
    }
}
