import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PostModel } from 'src/app/core/models/post.model';
import { PostCardComponent } from './post-card/post-card.component';
import { CreateCommentModel } from 'src/app/core/services/create.comment.model';

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
  @Output() createComment = new EventEmitter<{id: number, model: CreateCommentModel}>()
  @Output() likePost = new EventEmitter<number>()
  @Output() dislikePost = new EventEmitter<number>()

  onCreateComment(id: number, model: CreateCommentModel) {
    this.createComment.emit({id: id, model: model})
  }

  onLikePost(id: number) {
    this.likePost.emit(id)
  }

  onDislikePost(id: number) {
    this.dislikePost.emit(id)
  }

}
