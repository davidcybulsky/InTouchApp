import {Component, Input} from '@angular/core';
import {CommonModule} from '@angular/common';
import {UserModel} from 'src/app/core/models/user.model';
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {RoleConstants} from "../../../core/enums/role.constants";
import {faStar} from "@fortawesome/free-solid-svg-icons";

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
}
