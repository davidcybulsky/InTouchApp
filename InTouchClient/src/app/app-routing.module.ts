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
    path: "post",
    loadChildren: () => import('src/app/post/post.module').then(m => m.PostModule)
  },
  {
    path: "about",
    loadComponent: () => import('src/app/footer-sites/about/about.component').then(c => c.AboutComponent)
  },
  {
    path: "terms_of_use",
    loadComponent: () => import('src/app/footer-sites/terms-of-use/terms-of-use.component').then(c => c.TermsOfUseComponent)
  },
  {
    path: "privacy_policy",
    loadComponent: () => import('src/app/footer-sites/privacy-policy/privacy-policy.component').then(c => c.PrivacyPolicyComponent)
  },
  {
    path: "contact",
    loadComponent: () => import('src/app/footer-sites/contact/contact.component').then(c => c.ContactComponent)
  },
  {
    path: "often_asked_questions",
    loadComponent: () => import('src/app/footer-sites/questions/questions.component').then(c => c.QuestionsComponent)
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
