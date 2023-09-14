import {Component, Input} from '@angular/core';
import {CommonModule} from '@angular/common';
import {IncludeCommentModel} from 'src/app/core/models/include.comment.model';
import {ReactionService} from "../../../../core/services/reaction.service";
import {ReactionConstants} from "../../../../core/enums/reaction.constants";

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

  constructor(private reactionService: ReactionService) {
  }

  onLikeComment() {
    if(this.comment?.reactionsData.didIReacted == false) {
      this.reactionService.addCommentReaction(this.comment.id, ReactionConstants.LIKE).subscribe(success => {
        if(this.comment?.reactionsData) {
          this.comment.reactionsData.reactionType = ReactionConstants.LIKE
          this.comment.reactionsData.didIReacted = true
          this.comment.reactionsData.amountOfLikes++
        }
      })
    }
    else if(this.comment?.reactionsData.reactionType == ReactionConstants.DISLIKE) {
      this.reactionService.updateCommentReaction(this.comment.id, ReactionConstants.LIKE).subscribe(success =>{
        if(this.comment?.reactionsData) {
          this.comment.reactionsData.reactionType = ReactionConstants.LIKE
          this.comment.reactionsData.amountOfLikes++
          this.comment.reactionsData.amountOfDislikes--
        }
      })
    }
    else if(this.comment?.reactionsData.reactionType == ReactionConstants.LIKE) {
      this.reactionService.deleteCommentReaction(this.comment.id).subscribe(success => {
        if(this.comment?.reactionsData) {
          this.comment.reactionsData.reactionType = ""
          this.comment.reactionsData.didIReacted = false
          this.comment.reactionsData.amountOfLikes--
        }
      })
    }
  }

  onDislikeComment() {
    if(this.comment?.reactionsData.didIReacted == false) {
      this.reactionService.addCommentReaction(this.comment.id, ReactionConstants.DISLIKE).subscribe(success => {
        if(this.comment?.reactionsData) {
          this.comment.reactionsData.reactionType = ReactionConstants.DISLIKE
          this.comment.reactionsData.didIReacted = true
          this.comment.reactionsData.amountOfDislikes++
        }
      })
    }
    else if(this.comment?.reactionsData.reactionType == ReactionConstants.LIKE) {
      this.reactionService.updateCommentReaction(this.comment.id, ReactionConstants.DISLIKE).subscribe(success => {
        if(this.comment?.reactionsData) {
          this.comment.reactionsData.reactionType = ReactionConstants.DISLIKE
          this.comment.reactionsData.didIReacted = true
          this.comment.reactionsData.amountOfDislikes++
          this.comment.reactionsData.amountOfLikes--
        }
      })
    }
    else if(this.comment?.reactionsData.reactionType == ReactionConstants.DISLIKE) {
      this.reactionService.deleteCommentReaction(this.comment.id).subscribe(success => {
        if(this.comment?.reactionsData) {
          this.comment.reactionsData.reactionType = ""
          this.comment.reactionsData.didIReacted = false
          this.comment.reactionsData.amountOfDislikes--
        }
      })
    }
  }
}
