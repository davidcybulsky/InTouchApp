import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PostModel} from 'src/app/core/models/post.model';
import {RouterModule} from '@angular/router';
import {FormBuilder, FormGroup, FormsModule, NgForm, ReactiveFormsModule, Validators} from '@angular/forms';
import {CommentCardComponent} from '../../../comment/comment-card/comment-card.component';
import {CommentService} from "../../../../../core/services/comment.service";
import {ReactionService} from "../../../../../core/services/reaction.service";
import {ReactionConstants} from "../../../../../core/enums/reaction.constants";
import {faBars, faStar, faThumbsDown, faThumbsUp} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {AuthService} from "../../../../../core/services/auth.service";
import {PostService} from "../../../../../core/services/post.service";
import {RoleConstants} from "../../../../../core/enums/role.constants";

@Component({
  selector: 'app-post-card',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    CommentCardComponent,
    FontAwesomeModule,
    ReactiveFormsModule
  ],
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css']
})
export class PostCardComponent implements OnInit {

  @Input() post: PostModel | null = null
  @ViewChild('CommentForm') commentForm!: NgForm
  @Output() deletePost = new EventEmitter<number>()
  editPostForm!: FormGroup

  thumbsUp = faThumbsUp
  thumbsDown = faThumbsDown
  bar = faBars
  admin = faStar

  numberOfComments: number = 3
  canDisplayMoreComments: boolean = false
  displayOptions: boolean = false
  editMode: boolean = false
  protected readonly ReactionConstants = ReactionConstants;
  protected readonly RoleConstants = RoleConstants;

  constructor(private postService: PostService,
              private commentService: CommentService,
              private reactionService: ReactionService,
              public authService: AuthService,
              private formBuilder: FormBuilder) {
  }

  get mainPhotoUrl(): string | undefined {
    if (this.post && this.post.author && this.post.author.userPhoto) {
      const mainPhoto = this.post.author.userPhoto;
      return mainPhoto ? mainPhoto.url : undefined;
    }
    return undefined;
  }

  ngOnInit(): void {
    if (this.post?.comments)
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
        if (this.post?.reactionsData) {
          this.post.reactionsData.didIReacted = true
          this.post.reactionsData.reactionType = ReactionConstants.LIKE
          this.post.reactionsData.amountOfLikes++
        }
      })
    } else if (this.post?.reactionsData.reactionType == ReactionConstants.DISLIKE) {
      this.reactionService.updatePostReaction(this.post.id, ReactionConstants.LIKE).subscribe(success => {
        if (this.post?.reactionsData) {
          this.post.reactionsData.reactionType = ReactionConstants.LIKE
          this.post.reactionsData.amountOfLikes++
          this.post.reactionsData.amountOfDislikes--
        }
      })
    } else if (this.post?.reactionsData.reactionType == ReactionConstants.LIKE) {
      this.reactionService.deletePostReaction(this.post.id).subscribe(success => {
        if (this.post?.reactionsData) {
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
        if (this.post?.reactionsData) {
          this.post.reactionsData.didIReacted = true
          this.post.reactionsData.reactionType = ReactionConstants.DISLIKE
          this.post.reactionsData.amountOfDislikes++
        }
      })
    } else if (this.post?.reactionsData.reactionType == ReactionConstants.LIKE) {
      this.reactionService.updatePostReaction(this.post.id, ReactionConstants.DISLIKE).subscribe(success => {
        if (this.post?.reactionsData) {
          this.post.reactionsData.reactionType = ReactionConstants.DISLIKE
          this.post.reactionsData.amountOfDislikes++
          this.post.reactionsData.amountOfLikes--
        }
      })
    } else if (this.post?.reactionsData.reactionType == ReactionConstants.DISLIKE) {
      this.reactionService.deletePostReaction(this.post.id).subscribe(success => {
        if (this.post?.reactionsData) {
          this.post.reactionsData.reactionType = ""
          this.post.reactionsData.didIReacted = false
          this.post.reactionsData.amountOfDislikes--
        }
      })
    }
  }

  onClearForm() {
    if (confirm("Do you want to reset the form?"))
      this.commentForm.resetForm()
  }

  onShowMoreComments() {
    this.numberOfComments += 5
    if (this.post?.comments)
      this.canDisplayMoreComments = this.numberOfComments <= this.post?.comments?.length
  }

  onDeleteComment(commentId: number) {
    if (this.post)
      this.post!.comments = this.post?.comments.filter(c => c.id != commentId)
  }

  onEditPost() {
    this.editPostForm = this.formBuilder.group({
      title: [this.post?.title, [Validators.required]],
      content: [this.post?.content, [Validators.required]]
    })
    this.editMode = true
  }

  onConfirmEditPost() {
    if (this.post)
      this.postService.updatePost(this.post?.id, this.editPostForm.value).subscribe(success => {
        if (this.post) {
          this.post.title = this.editPostForm.get("title")?.value
          this.post.content = this.editPostForm.get("content")?.value
          this.editMode = false
        }
      })
  }

  onCancelEditPost() {
    if (this.editPostForm.touched)
      if (confirm("Do you want to cancel edition?")) {
        this.editMode = false
      }
  }

  onDeletePost() {
    if (this.post)
      if (confirm("Do you want to delete the post?")) {
        this.postService.deletePost(this.post?.id).subscribe(success => {
            this.deletePost.emit(this.post?.id)
          }
        )
      }
  }
}
