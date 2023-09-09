import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IncludeCommentModel } from 'src/app/core/models/include.comment.model';

@Component({
  selector: 'app-comment-card',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './comment-card.component.html',
  styleUrls: ['./comment-card.component.css']
})
export class CommentCardComponent {
  
  @Input() comment: IncludeCommentModel | null = null
  @Output() likeComment = new EventEmitter<number>()
  @Output() dislikeComment = new EventEmitter<number>()

  onLikeComment() {
    this.dislikeComment.emit(this.comment?.id)
  }

  onDislikeComment() {
    this.dislikeComment.emit(this.comment?.id)
  }
}
