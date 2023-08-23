import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  standalone: true,
  selector: 'app-login',
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;

  constructor(private authService: AuthService, private formBuilder: FormBuilder) {

  }

  ngOnInit(): void {
    this.InitForm();
  }
  
  InitForm() {
    this.loginForm = this.formBuilder.group({
      email: [''],
      password: ['']
    });
  }

  onLogin() {
    console.log(this.loginForm.value);
    this.authService.login(this.loginForm.value).subscribe();
  }

}
