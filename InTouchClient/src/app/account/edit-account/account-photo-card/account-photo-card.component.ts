import {Component, EventEmitter, Input, Output} from '@angular/core';
import {IncludePhotoModel} from "../../../core/models/include.photo.model";
import {NgIf} from "@angular/common";

@Component({
  standalone: true,
  imports: [
    NgIf
  ],
  selector: 'app-account-photo-card',
  templateUrl: './account-photo-card.component.html',
  styleUrls: ['./account-photo-card.component.css']
})
export class AccountPhotoCardComponent {
  @Input() photo: IncludePhotoModel | null = null
  @Output() deletePhoto = new EventEmitter<number>();
  @Output() setMainPhoto = new EventEmitter<number>();

  onSetMain() {
    this.setMainPhoto.emit(this.photo?.id)
  }

  onDelete() {
    this.deletePhoto.emit(this.photo?.id)
  }
}
