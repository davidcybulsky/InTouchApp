import { NgModule } from '@angular/core';
import {RouterModule, Routes } from '@angular/router';

const routes : Routes = [
  {
    path: "",
    redirectTo: "login", 
    pathMatch:"full"
  },
  {
    path: "login",
    loadChildren: () => import('src/app/auth/login/login.module')
      .then(m => m.LoginModule)
  },
  {
    path: "signup",
    loadChildren: () => import('src/app/auth/signup/signup.module')
      .then(m => m.SignupModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
