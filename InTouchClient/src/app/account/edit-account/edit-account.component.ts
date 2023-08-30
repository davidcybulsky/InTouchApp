import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AccountService } from 'src/app/core/services/account.service';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ],
  selector: 'app-edit-account',
  templateUrl: './edit-account.component.html',
  styleUrls: ['./edit-account.component.css']
})
export class EditAccountComponent implements OnInit, OnDestroy {

  editAccountForm! : FormGroup

  constructor(private accountService: AccountService,
              private formBuilder: FormBuilder,
              private router: Router) { }

  ngOnInit(): void {
    this.InitForm()
    this.accountService.getAccount().subscribe(
      account => {
        console.log(account)
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
        }})
      })
  }

  ngOnDestroy(): void {

  }

  InitForm(): void {
    this.editAccountForm = this.formBuilder.group({
      firstName: [''],
      lastName: [''],
      phoneNumber: [''],
      description: [''],
      facebookURL: [''],
      instagramURL: [''],
      linkedInURL: [''],
      tikTokURL: [''],
      youTubeURL: [''],
      twitterURL: [''],
      address: this.formBuilder.group({
        localNumber: [],
        buildingNumber: [],
        street: [''],
        zipCode: [''],
        city: [''],
        region: [''],
        country: ['']
    })})
  }

  onEditAccount() {
    this.accountService.updateAccount(this.editAccountForm.value).subscribe()
  }

  onCancel() {
    this.router.navigate(['/account'])
  }

  onDelete() {
    let decision = confirm("Do you want to delete your account?")
    if(decision) {
      this.accountService.deleteAccount()
    }
  }
}
