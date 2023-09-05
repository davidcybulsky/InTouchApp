import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  standalone: true,
  selector: 'app-login',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup
  isSubmitted: boolean | null = null;

  constructor(private authService: AuthService, 
              private formBuilder: FormBuilder,
              private router: Router) { }

  ngOnInit(): void {
    this.isSubmitted = false
    this.InitForm()
  }
  
  InitForm() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    })
  }

  onLogin() {
    console.log(this.loginForm.value)
    this.isSubmitted = true
    this.authService.login(this.loginForm.value).subscribe()
  }

  onSignup() {
    this.router.navigate(['auth', 'signup'])
  }

}
