import {CommonModule} from '@angular/common';
import {Component, Inject, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {Router, RouterModule} from '@angular/router';
import {AccountService} from 'src/app/core/services/account.service';
import {AuthService} from "../../core/services/auth.service";
import {PhotoService} from "../../core/services/photo.service";
import {FileUploader, FileUploadModule} from "ng2-file-upload";
import {ENVIRONMENT_TOKEN} from "../../core/tokens/environment.token";
import {IEnvoronment} from "../../../environment/environment.interface";
import {PhotoServiceEndpoints} from "../../core/enums/photo.service.endpoints";
import {AccountPhotoCardComponent} from "./account-photo-card/account-photo-card.component";
import {AccountModel} from "../../core/models/account.model";
import {IncludeUserPhotoModel} from "../../core/models/include.user.photo.model";

@Component({
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    FileUploadModule,
    AccountPhotoCardComponent
  ],
  selector: 'app-edit-account',
  templateUrl: './edit-account.component.html',
  styleUrls: ['./edit-account.component.css']
})
export class EditAccountComponent implements OnInit, OnDestroy {

  editAccountForm!: FormGroup
  private selectedPhoto: any = null;
  account: AccountModel | null = null;

  uploader!: FileUploader;
  hasBaseDropZoneOver = false;

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private accountService: AccountService,
              private formBuilder: FormBuilder,
              private router: Router,
              private photoService: PhotoService,
              private authService: AuthService) {
  }

  ngOnInit(): void {
    this.initializeUploader()
    this.InitForm()
    this.accountService.getAccount().subscribe(
      account => {
        console.log(account)
        this.account = account
        this.editAccountForm.patchValue({
          firstName: account.firstName,
          lastName: account.lastName,
          phoneNumber: account.phoneNumber,
          description: account.description,
          facebookURL: account.facebookURL,
          instagramURL: account.instagramURL,
          linkedInURL: account.linkedInURL,
          tikTokURL: account.tikTokURL,
          youTubeURL: account.youTubeURL,
          twitterURL: account.twitterURL,
          address: {
            localNumber: account.address.localNumber,
            buildingNumber: account.address.buildingNumber,
            street: account.address.street,
            zipCode: account.address.zipCode,
            city: account.address.city,
            region: account.address.region,
            country: account.address.country
          }
        })
      })
  }

  ngOnDestroy(): void {

  }

  InitForm(): void {
    this.editAccountForm = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      description: [''],
      facebookURL: [''],
      instagramURL: [''],
      linkedInURL: [''],
      tikTokURL: [''],
      youTubeURL: [''],
      twitterURL: [''],
      address: this.formBuilder.group({
        localNumber: [],
        buildingNumber: ['', [Validators.required]],
        street: ['', [Validators.required]],
        zipCode: ['', [Validators.required]],
        city: ['', [Validators.required]],
        region: ['', [Validators.required]],
        country: ['', [Validators.required]]
      })
    })
  }

  onEditAccount() {
    if(this.uploader.queue.length > 0) {
      if(confirm("Do you want to update your account? THe queue is no empty!")) {
        this.accountService.updateAccount(this.editAccountForm.value).subscribe(success => {
          this.router.navigate(["/account"])
        })
      }
    } else {
      this.accountService.updateAccount(this.editAccountForm.value).subscribe(success => {
        this.router.navigate(["/account"])
      })
    }

  }

  onCancel() {
    if (this.editAccountForm.touched) {
      if (confirm("Do you want to cancel?")) {
        this.router.navigate(['/account'])
      }
    } else {
      this.router.navigate(['/account'])
    }
  }

  onDelete() {
    let decision = confirm("Do you want to delete your account?")
    if (decision) {
      this.accountService.deleteAccount().subscribe(success => {
        this.authService.logout()
      })
    }
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: `${this.ENVIRONMENT_TOKEN.apiUrl}${PhotoServiceEndpoints.ADD_USER_PHOTO}`,
      authToken: this.authService.getJWTTokenData()?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    })

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false
    }

    this.uploader.onSuccessItem = (item, response, status, header) => {
      if (response) {
        let photo = JSON.parse(response)
        this.account?.userPhotos.unshift(photo)
        console.log(this.account?.userPhotos)
      }
    }
  }

  onSetMainPhoto(photoId: number) {
    console.log(photoId)
    this.photoService.setMainUserPhoto(photoId).subscribe( response =>{
      console.log("setmain")
      if(this.account?.userPhotos) {
        this.account.userPhotos.map(p => p.isMain = false)
        let photo = this.account.userPhotos.find(p => p.id == photoId)
        if(photo) {
          photo.isMain = true
        }
      }
      })
  }

  onDeletePhoto(photoId: number) {
    console.log(photoId)
    this.photoService.deleteUserPhoto(photoId).subscribe(response => {
      console.log("deleted")
      if(this.account?.userPhotos) {
        this.account.userPhotos = this.account.userPhotos.filter(p => p.id != photoId)
      }
    })
  }

}
