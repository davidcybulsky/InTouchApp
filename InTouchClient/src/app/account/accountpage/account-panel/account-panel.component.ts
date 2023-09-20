import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AccountModel} from 'src/app/core/models/account.model';
import {faBars} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-account-panel',
  standalone: true,
  imports: [CommonModule, FontAwesomeModule, RouterLink],
  templateUrl: './account-panel.component.html',
  styleUrls: ['./account-panel.component.css']
})
export class AccountPanelComponent {
  @Input() account: AccountModel | null = null;
  @Output() editAccount = new EventEmitter<void>();
  public bar = faBars;
  displayOptions: boolean = false;

  get mainPhotoUrl(): string | undefined {
    if (this.account && this.account.userPhotos) {
      const mainPhoto = this.account.userPhotos.find(p => p.isMain);
      return mainPhoto ? mainPhoto.url : undefined;
    }
    return undefined;
  }
}
