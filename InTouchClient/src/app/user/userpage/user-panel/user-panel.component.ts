import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserModel } from 'src/app/core/models/user.model';

@Component({
  selector: 'app-user-panel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent {
  @Input() user : UserModel | null = null;
}
