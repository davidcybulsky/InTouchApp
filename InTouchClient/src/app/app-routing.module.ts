import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: "", 
    loadComponent: () => import('src/app/homepage/homepage.component').then(c => c.HomepageComponent)
  },
  {
    path: "auth", 
    loadChildren: () => import('src/app/auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: "user",
    loadChildren: () => import('src/app/user/user.module').then(m => m.UserModule)
  },
  {
    path: "account",
    loadChildren: () => import('src/app/account/account.module').then(m => m.AccountModule)
  },
  {
    path: "**",
    redirectTo: "",
    pathMatch: "full"
  }

];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
