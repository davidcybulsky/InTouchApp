import { Component, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostModel } from 'src/app/core/models/post.model';
import { RouterModule } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { CommentService } from 'src/app/core/services/comment.service';
import { ReactionService } from 'src/app/core/services/reaction.service';
import { CommentCardComponent } from '../../../comment/comment-card/comment-card.component';

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
  @Input() post : PostModel | null = null
  @ViewChild('CommentForm') commentForm!: NgForm

  constructor(private commentService: CommentService,
              private reactionService: ReactionService) { }

  onCommentCreate() {
    if(this.post) {
      this.commentService.createPostComment(this.post?.id,this.commentForm.value).subscribe()
    }
  }

  onLikePost() { 
    if(this.post) {
      this.reactionService.addPostLike(this.post?.id)
    }
  }

  onDislikePost() {
    if(this.post) {
      this.reactionService.addPostDisLike(this.post?.id)
    }
  }
}