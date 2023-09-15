import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SearchRoutingModule } from './search-routing.module';
import {HeaderComponent} from "../shared/components/header/header.component";
import {SearchpageComponent} from "./searchpage/searchpage.component";
import {UsersSearchComponent} from "./searchpage/users-search/users-search.component";
import {FriendsSearchComponent} from "./searchpage/friends-search/friends-search.component";
import {PostsSearchComponent} from "./searchpage/posts-search/posts-search.component";
import {ReactiveFormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    SearchpageComponent,
    UsersSearchComponent,
    FriendsSearchComponent,
    PostsSearchComponent
  ],
  imports: [
    CommonModule,
    SearchRoutingModule,
    HeaderComponent,
    ReactiveFormsModule
  ]
})
export class SearchModule { }
