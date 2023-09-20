import {Component, Input, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {UserModel} from 'src/app/core/models/user.model';
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {RoleConstants} from "../../../core/enums/role.constants";
import {faStar} from "@fortawesome/free-solid-svg-icons";
import {IncludeUserPhotoModel} from "../../../core/models/include.user.photo.model";

@Component({
  selector: 'app-user-panel',
  standalone: true,
  imports: [CommonModule, FontAwesomeModule],
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent {
  @Input() user: UserModel | null = null
  protected readonly RoleConstants = RoleConstants
  protected readonly admin = faStar

  get mainPhotoUrl(): string | undefined {
    if (this.user && this.user.userPhotos) {
      const mainPhoto = this.user.userPhotos.find(p => p.isMain);
      return mainPhoto ? mainPhoto.url : undefined;
    }
    return undefined;
  }

}
