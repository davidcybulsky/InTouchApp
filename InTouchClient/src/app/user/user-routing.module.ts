import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CanActivateUserPageGuard} from "../core/guards/can-activate-user-page.guard";

const routes: Routes = [
  {
    path: ":id",
    loadComponent: () => import('src/app/user/userpage/userpage.component').then(c => c.UserpageComponent),
    canActivate: [CanActivateUserPageGuard]
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
