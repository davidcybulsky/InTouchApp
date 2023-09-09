import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostModel } from 'src/app/core/models/post.model';
import { RouterModule } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { CommentService } from 'src/app/core/services/comment.service';
import { ReactionService } from 'src/app/core/services/reaction.service';
import { CommentCardComponent } from '../../../comment/comment-card/comment-card.component';
import { CreateCommentModel } from 'src/app/core/models/create.comment.model';

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
  @Output() createComment = new EventEmitter<{id: number, model: CreateCommentModel}>()
  @Output() likePost = new EventEmitter<number>()
  @Output() dislikePost = new EventEmitter<number>()

  onCreateComment() {
    if(this.post) {
      this.createComment.emit({id: this.post.id, model: this.commentForm.value})
    }
  }

  onLikePost() { 
    if(this.post) {
      this.likePost.emit(this.post.id)
    }
  }

  onDislikePost() {
    if(this.post) {
      this.dislikePost.emit(this.post.id)
    }
  }
}