import {Component, destroyPlatform, EventEmitter, Input, Output} from '@angular/core';
import {CommonModule} from '@angular/common';
import {IncludeCommentModel} from 'src/app/core/models/include.comment.model';
import {ReactionService} from "../../../../core/services/reaction.service";
import {ReactionConstants} from "../../../../core/enums/reaction.constants";
import {faBars, faThumbsDown, faThumbsUp} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {RouterLink} from "@angular/router";
import {AuthService} from "../../../../core/services/auth.service";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {CommentService} from "../../../../core/services/comment.service";

@Component({
  selector: 'app-comment-card',
  standalone: true,
  imports: [
    CommonModule,
    FontAwesomeModule,
    RouterLink,
    ReactiveFormsModule
  ],
  templateUrl: './comment-card.component.html',
  styleUrls: ['./comment-card.component.css']
})
export class CommentCardComponent {

  @Input() comment: IncludeCommentModel | null = null

  editCommentForm! : FormGroup
  thumbsUp = faThumbsUp
  thumbsDown = faThumbsDown
  bar = faBars
  protected readonly ReactionConstants = ReactionConstants
  displayOptions: boolean = false
  editMode: boolean = false
  @Output() deleteComment = new EventEmitter<number>()

  constructor(private reactionService: ReactionService,
              public authService: AuthService,
              private formBuilder: FormBuilder,
              private commentService: CommentService) {
  }

  onLikeComment() {
    if (this.comment?.reactionsData.didIReacted == false) {
      this.reactionService.addCommentReaction(this.comment.id, ReactionConstants.LIKE).subscribe(success => {
        if (this.comment?.reactionsData) {
          this.comment.reactionsData.reactionType = ReactionConstants.LIKE
          this.comment.reactionsData.didIReacted = true
          this.comment.reactionsData.amountOfLikes++
        }
      })
    } else if (this.comment?.reactionsData.reactionType == ReactionConstants.DISLIKE) {
      this.reactionService.updateCommentReaction(this.comment.id, ReactionConstants.LIKE).subscribe(success => {
        if (this.comment?.reactionsData) {
          this.comment.reactionsData.reactionType = ReactionConstants.LIKE
          this.comment.reactionsData.amountOfLikes++
          this.comment.reactionsData.amountOfDislikes--
        }
      })
    } else if (this.comment?.reactionsData.reactionType == ReactionConstants.LIKE) {
      this.reactionService.deleteCommentReaction(this.comment.id).subscribe(success => {
        if (this.comment?.reactionsData) {
          this.comment.reactionsData.reactionType = ""
          this.comment.reactionsData.didIReacted = false
          this.comment.reactionsData.amountOfLikes--
        }
      })
    }
  }

  onDislikeComment() {
    if (this.comment?.reactionsData.didIReacted == false) {
      this.reactionService.addCommentReaction(this.comment.id, ReactionConstants.DISLIKE).subscribe(success => {
        if (this.comment?.reactionsData) {
          this.comment.reactionsData.reactionType = ReactionConstants.DISLIKE
          this.comment.reactionsData.didIReacted = true
          this.comment.reactionsData.amountOfDislikes++
        }
      })
    } else if (this.comment?.reactionsData.reactionType == ReactionConstants.LIKE) {
      this.reactionService.updateCommentReaction(this.comment.id, ReactionConstants.DISLIKE).subscribe(success => {
        if (this.comment?.reactionsData) {
          this.comment.reactionsData.reactionType = ReactionConstants.DISLIKE
          this.comment.reactionsData.didIReacted = true
          this.comment.reactionsData.amountOfDislikes++
          this.comment.reactionsData.amountOfLikes--
        }
      })
    } else if (this.comment?.reactionsData.reactionType == ReactionConstants.DISLIKE) {
      this.reactionService.deleteCommentReaction(this.comment.id).subscribe(success => {
        if (this.comment?.reactionsData) {
          this.comment.reactionsData.reactionType = ""
          this.comment.reactionsData.didIReacted = false
          this.comment.reactionsData.amountOfDislikes--
        }
      })
    }
  }

  onEdit() {
    this.editCommentForm = this.formBuilder.group({
      content: [this.comment?.content, [Validators.required]]
    })
    this.editMode = true
  }

  onConfirmEdit() {
    if(this.comment) {
      this.commentService.updatePostComment(this.comment?.id, this.editCommentForm.value).subscribe(success => {
        let content = this.editCommentForm.get("content")?.value
        content = content.toString()
        this.comment!.content = content
        this.editMode = false
      })
    }
  }

  onCancelEdit() {
    if(this.editCommentForm.touched){
      if(confirm("Do you want to cancel edition?")){
        this.editMode = false
    }
    }
  }

  onDelete() {
    if(this.comment) {
      if(confirm("Do you want to delete the comment?")) {
        this.commentService.deletePostComment(this.comment?.id).subscribe(success => {
          if (this.comment)
            this.deleteComment.emit(this.comment.id)
        })
      }
    }
  }
}
