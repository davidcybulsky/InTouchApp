import { Component, Input, OnInit } from '@angular/core';
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
  @Input() comment: IncludeCommentModel | null = null;
}
