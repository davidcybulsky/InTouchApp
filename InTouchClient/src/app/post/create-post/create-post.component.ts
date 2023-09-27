import {Component, Inject, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule} from '@angular/forms';
import {PostService} from 'src/app/core/services/post.service';
import {Router, RouterModule} from '@angular/router';
import {PhotoService} from "../../core/services/photo.service";
import {FileUploader, FileUploadModule} from "ng2-file-upload";
import {AuthService} from "../../core/services/auth.service";
import {ENVIRONMENT_TOKEN} from "../../core/tokens/environment.token";
import {IEnvoronment} from "../../../environment/environment.interface";
import {PhotoServiceEndpoints} from "../../core/enums/photo.service.endpoints";

@Component({
  selector: 'app-create-post',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    FileUploadModule
  ],
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {
  createPostForm!: FormGroup
  selectedFile = null;

  uploader!: FileUploader;
  hasBaseDropZoneOver: boolean = false;

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private postService: PostService,
              private formBuilder: FormBuilder,
              private photoService: PhotoService,
              private router: Router,
              private authService: AuthService) {
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
        if(this.selectedFile){
          this.photoService.addPostPhoto(this.selectedFile, success.id).subscribe(
            success => {
              if(this.uploader?.queue?.length) {
                this.uploader.uploadAll()
              }
              this.router.navigate([""])
            }
          )
        }
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

  fileOverBase(e : any) {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: `${this.ENVIRONMENT_TOKEN.apiUrl}${PhotoServiceEndpoints.ADD_POST_PHOTO}`,
      authToken: this.authService.getJWTTokenData()?.token,
      isHTML5 : true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10*1024*1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false
    }

    this.uploader.onSuccessItem = (item, response, status, header) => {
      if(response) {
        const photo = JSON.parse(response)
      }
    }
  }


}
