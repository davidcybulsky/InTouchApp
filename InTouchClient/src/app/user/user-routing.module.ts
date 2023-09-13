import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

const routes: Routes = [
  {
    path: ":id",
    loadComponent: () => import('src/app/user/userpage/userpage.component').then(c => c.UserpageComponent)
  },
  {
    path: "edit",
    loadComponent: () => import('src/app/user/edit-user/edit-user.component')
      .then(c => c.EditUserComponent)
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule {
}
