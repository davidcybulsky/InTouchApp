import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {SearchpageComponent} from "./searchpage/searchpage.component";
import {UsersSearchComponent} from "./searchpage/users-search/users-search.component";
import {FriendsSearchComponent} from "./searchpage/friends-search/friends-search.component";
import {PostsSearchComponent} from "./searchpage/posts-search/posts-search.component";

const routes: Routes = [
  {
    path: "",
    component: SearchpageComponent,
    children: [
      {
        path: "user",
        component: UsersSearchComponent
      },
      {
        path: "friend",
        component: FriendsSearchComponent
      },
      {
        path: "post",
        component: PostsSearchComponent
      },
      {
        path: "",
        redirectTo: "user",
        pathMatch: "full"
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SearchRoutingModule { }
