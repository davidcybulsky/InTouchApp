import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AccountModel} from 'src/app/core/models/account.model';

@Component({
  selector: 'app-account-panel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './account-panel.component.html',
  styleUrls: ['./account-panel.component.css']
})
export class AccountPanelComponent {
  @Input() account: AccountModel | null = null;
  @Output() editAccount = new EventEmitter<void>();

  onEditAccount() {
    console.log("emitted");

    this.editAccount.emit();
  }
}
