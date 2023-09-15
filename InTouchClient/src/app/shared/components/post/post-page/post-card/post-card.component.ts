import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PostModel} from 'src/app/core/models/post.model';
import {RouterModule} from '@angular/router';
import {FormsModule, NgForm} from '@angular/forms';
import {CommentCardComponent} from '../../../comment/comment-card/comment-card.component';
import {CommentService} from "../../../../../core/services/comment.service";
import {ReactionService} from "../../../../../core/services/reaction.service";
import {ReactionConstants} from "../../../../../core/enums/reaction.constants";
import {faThumbsUp, faThumbsDown, faBars} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {AuthService} from "../../../../../core/services/auth.service";

@Component({
  selector: 'app-post-card',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    CommentCardComponent,
    FontAwesomeModule
  ],
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css']
})
export class PostCardComponent implements OnInit{

  @Input() post: PostModel | null = null
  @ViewChild('CommentForm') commentForm!: NgForm

  thumbsUp = faThumbsUp
  thumbsDown = faThumbsDown
  bar = faBars

  numberOfComments: number = 3;
  canDisplayMoreComments: boolean = false;


  constructor(private commentService: CommentService,
              private reactionService: ReactionService,
              public authService: AuthService) {
  }

  ngOnInit(): void {
    if(this.post?.comments)
        this.canDisplayMoreComments = this.numberOfComments <= this.post?.comments?.length
    }

  onCreateComment() {
    if (this.post) {
      const form = this.commentForm.value
      this.commentForm.resetForm()
      this.commentService.createPostComment(this.post.id, form)
        .subscribe(response => {
          console.log(response)
          this.post?.comments.unshift(response)
        })
    }
  }

  onLikePost() {
    if (this.post?.reactionsData.didIReacted == false) {
      this.reactionService.addPostReaction(this.post.id, ReactionConstants.LIKE).subscribe(success => {
        if(this.post?.reactionsData) {
          this.post.reactionsData.didIReacted = true
          this.post.reactionsData.reactionType = ReactionConstants.LIKE
          this.post.reactionsData.amountOfLikes++
        }
      })
    }
    else if(this.post?.reactionsData.reactionType == ReactionConstants.DISLIKE) {
      this.reactionService.updatePostReaction(this.post.id, ReactionConstants.LIKE).subscribe(success => {
        if(this.post?.reactionsData) {
          this.post.reactionsData.reactionType = ReactionConstants.LIKE
          this.post.reactionsData.amountOfLikes++
          this.post.reactionsData.amountOfDislikes--
        }
      })
    }
    else if(this.post?.reactionsData.reactionType == ReactionConstants.LIKE) {
      this.reactionService.deletePostReaction(this.post.id).subscribe(success => {
        if(this.post?.reactionsData) {
          this.post.reactionsData.reactionType = ""
          this.post.reactionsData.didIReacted = false
          this.post.reactionsData.amountOfLikes--
        }
      })
    }
  }

  onDislikePost() {
    if (this.post?.reactionsData.didIReacted == false) {
      this.reactionService.addPostReaction(this.post.id, ReactionConstants.DISLIKE).subscribe(success => {
        if(this.post?.reactionsData) {
          this.post.reactionsData.didIReacted = true
          this.post.reactionsData.reactionType = ReactionConstants.DISLIKE
          this.post.reactionsData.amountOfDislikes++
        }
      })
    }
    else if(this.post?.reactionsData.reactionType == ReactionConstants.LIKE) {
      this.reactionService.updatePostReaction(this.post.id, ReactionConstants.DISLIKE).subscribe(success => {
        if(this.post?.reactionsData) {
          this.post.reactionsData.reactionType = ReactionConstants.DISLIKE
          this.post.reactionsData.amountOfDislikes++
          this.post.reactionsData.amountOfLikes--
        }
      })
    }
    else if(this.post?.reactionsData.reactionType == ReactionConstants.DISLIKE) {
      this.reactionService.deletePostReaction(this.post.id).subscribe(success => {
        if(this.post?.reactionsData) {
          this.post.reactionsData.reactionType = ""
          this.post.reactionsData.didIReacted = false
          this.post.reactionsData.amountOfDislikes--
        }
      })
    }
  }

  onClearForm() {
    if(confirm("Do you want to reset the form?"))
      this.commentForm.resetForm()
  }

  onShowMoreComments() {
    this.numberOfComments += 5
    if(this.post?.comments)
      this.canDisplayMoreComments = this.numberOfComments <= this.post?.comments?.length
  }

  protected readonly ReactionConstants = ReactionConstants;
}
