import {Component, Input, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FriendModel} from 'src/app/core/models/friend.model';
import {RouterModule} from '@angular/router';
import {faDotCircle, faMessage} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {AuthService} from "../../../../../core/services/auth.service";
import {MessageService} from "../../../../../core/services/message.service";
import {ConnectionService} from "../../../../../core/services/connection.service";

@Component({
  selector: 'app-friend-card',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FontAwesomeModule
  ],
  templateUrl: './friend-card.component.html',
  styleUrls: ['./friend-card.component.css']
})
export class FriendCardComponent implements OnInit{

  @Input() friend?: FriendModel
  message = faMessage
  openMessageWindow: boolean = false

  constructor(protected connectionService: ConnectionService) {
  }

  ngOnInit(): void {
  }

  protected readonly String = String;
  protected readonly faDotCircle = faDotCircle;
}
