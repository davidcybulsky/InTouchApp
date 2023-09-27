import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {AccountRoutingModule} from './account-routing.module';
import { AccountPhotoCardComponent } from './edit-account/account-photo-card/account-photo-card.component';


@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    AccountRoutingModule
  ]
})
export class AccountModule {
}
