import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: "", 
    loadChildren: () => import('src/app/user/userpage/userpage.module').then(m => m.UserpageModule)
  },
  {
    path: "edit",
    loadChildren: () => import('src/app/user/edit-user/edit-user.module')
    .then(c => c.EditUserModule)
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
