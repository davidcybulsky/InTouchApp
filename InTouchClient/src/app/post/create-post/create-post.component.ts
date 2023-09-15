import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule} from '@angular/forms';
import {PostService} from 'src/app/core/services/post.service';
import {Router, RouterModule} from '@angular/router';

@Component({
  selector: 'app-create-post',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule
  ],
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {
  createPostForm!: FormGroup

  constructor(private postService: PostService,
              private formBuilder: FormBuilder,
              private router: Router) {
  }

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
      this.postService.createPost(this.createPostForm.value).subscribe(success => {
        this.router.navigate([""])
      })
  }

  onCancel() {
    if(this.createPostForm.touched) {
      if(confirm("Do you want to cancel?"))
        this.router.navigate([''])
    }
    else {
      this.router.navigate([''])
    }
  }

}
