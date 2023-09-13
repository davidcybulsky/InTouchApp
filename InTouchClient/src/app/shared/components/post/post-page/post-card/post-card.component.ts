import {Component, Input, ViewChild} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PostModel} from 'src/app/core/models/post.model';
import {RouterModule} from '@angular/router';
import {FormsModule, NgForm} from '@angular/forms';
import {CommentCardComponent} from '../../../comment/comment-card/comment-card.component';
import {CommentService} from "../../../../../core/services/comment.service";
import {ReactionService} from "../../../../../core/services/reaction.service";

@Component({
  selector: 'app-post-card',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    CommentCardComponent
  ],
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css']
})
export class PostCardComponent {
  @Input() post: PostModel | null = null
  @ViewChild('CommentForm') commentForm!: NgForm

  constructor(private commentService: CommentService,
              private reactionService: ReactionService) {
  }

  onCreateComment() {
    if (this.post) {
      this.commentService.createPostComment(this.post.id,this.commentForm.value)
        .subscribe(success => {
        })
    }
  }

  onLikePost() {
    if (this.post) {
      this.reactionService.addPostLike(this.post.id)
    }
  }

  onDislikePost() {
    if (this.post) {
      this.reactionService.addPostDisLike(this.post.id)
    }
  }
}
