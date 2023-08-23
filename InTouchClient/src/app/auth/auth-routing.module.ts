import { NgModule } from '@angular/core';
import {RouterModule, Routes } from '@angular/router';

const routes : Routes = [
  {
    path: "login",
    loadComponent: () => import('src/app/auth/login/login.component')
      .then(c => c.LoginComponent)
  },
  {
    path: "signup",
    loadComponent: () => import('src/app/auth/signup/signup.component')
      .then(c => c.SignupComponent)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
