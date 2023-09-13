import {CommonModule} from '@angular/common';
import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {Router, RouterModule} from '@angular/router';

import {AuthService} from 'src/app/core/services/auth.service';
import {ComparePasswords} from 'src/app/shared/validators/compare.passwords';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule
  ],
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  signUpForm!: FormGroup
  isSubmitted: boolean | null = null;

  constructor(private authService: AuthService,
              private formBuilder: FormBuilder,
              private router: Router) {
  }

  ngOnInit(): void {
    this.isSubmitted = false
    this.InitForm()
  }

  InitForm() {
    this.signUpForm = this.formBuilder.group({
        email: ['', [Validators.email, Validators.required]],
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required, Validators.minLength(8)]],
        firstName: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        birthDate: ['', [Validators.required]],
        phoneNumber: ['', [Validators.required]],
        description: [''],
        address: this.formBuilder.group({
          localNumber: [''],
          buildingNumber: ['', [Validators.required]],
          street: ['', [Validators.required]],
          zipCode: ['', [Validators.required]],
          city: ['', [Validators.required]],
          region: ['', [Validators.required]],
          country: ['', [Validators.required]]
        })
      },
      {
        validator: ComparePasswords("password", "confirmPassword")
      });
  }

  onSignUp() {
    this.isSubmitted = true
    this.authService.signup(this.signUpForm.value)
  }

  onCancel() {
    this.router.navigate(['auth', 'login'])
  }
}
