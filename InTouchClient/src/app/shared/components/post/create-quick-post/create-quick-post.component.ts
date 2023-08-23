import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { PostService } from 'src/app/core/services/post.service';
import { CreateQuickPostModel } from 'src/app/core/models/create.quick.post.model';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-create-quick-post',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule
  ],
  templateUrl: './create-quick-post.component.html',
  styleUrls: ['./create-quick-post.component.css']
})
export class CreateQuickPostComponent implements OnInit {

  quickPostForm!: FormGroup;

  constructor(private postService: PostService,
              private formBuilder: FormBuilder) { }
  
  ngOnInit(): void {
    this.quickPostForm = this.formBuilder.group({
      title: [''],
      content: ['']
    })
  }

  onCreateQuickPost(){
    this.postService.createQuickPost(this.quickPostForm.value)
  }

}
