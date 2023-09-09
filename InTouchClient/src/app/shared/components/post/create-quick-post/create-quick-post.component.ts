import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { PostService } from 'src/app/core/services/post.service';
import { RouterModule } from '@angular/router';
import { CreatePostModel } from 'src/app/core/models/create.post.model';

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
  @Output() CreateQuickPost = new EventEmitter<CreatePostModel>();

  constructor(private formBuilder: FormBuilder) { }
  
  ngOnInit(): void {
    this.quickPostForm = this.formBuilder.group({
      title: [''],
      content: ['']
    })
  }

  onCreateQuickPost(){
    this.CreateQuickPost.emit(this.quickPostForm.value);
  }

}
