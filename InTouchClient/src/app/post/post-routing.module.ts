import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: "",
    loadComponent: () => import('src/app/post/postpage/postpage.component')
      .then(c => c.PostpageComponent)
  },
  {
    path: "create",
    loadComponent: () => import('src/app/post/create-post/create-post.component')
      .then(c => c.CreatePostComponent)
  },
  {
    path: "edit",
    loadComponent: () => import('src/app/post/edit-post/edit-post.component')
      .then(c => c.EditPostComponent)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostRoutingModule { }