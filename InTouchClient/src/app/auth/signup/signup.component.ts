import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  signUpForm! : FormGroup

  constructor(private authService: AuthService, 
              private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.InitForm()
  }

  InitForm() {
    this.signUpForm = this.formBuilder.group({
      email: [''],
      password: [''],
      confirmPassword: ['']
    });
  }

  onSignUp(){
    this.authService.signup(this.signUpForm.value)
  }
}
