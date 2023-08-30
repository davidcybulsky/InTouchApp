import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PostService } from 'src/app/core/services/post.service';

@Component({
  selector: 'app-create-post',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {
  createPostForm!: FormGroup

  constructor(private postService: PostService,
              private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.InitForm()
  }

  InitForm(): void {
    this.createPostForm = this.formBuilder.group({
      title: [''],
      content: ['']
    })
  }

  onCreatePost(): void {
    this.postService.createPost(this.createPostForm.value)
  }

}
