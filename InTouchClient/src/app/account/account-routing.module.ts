import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

const routes: Routes = [
  {
    path: "",
    loadComponent: () => import('src/app/account/accountpage/accountpage.component')
      .then(c => c.AccountpageComponent)
  },
  {
    path: "edit",
    loadComponent: () => import('src/app/account/edit-account/edit-account.component')
      .then(c => c.EditAccountComponent)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule {
}
