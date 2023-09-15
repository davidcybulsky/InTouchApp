import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {TokenModel} from '../models/token.model';
import {IEnvoronment} from 'src/environment/environment.interface';
import {AuthServiceEndpoints} from '../enums/auth.service.endpoints';
import {map, Observable, of} from 'rxjs';
import {StorageService} from './storage.service';
import {StorageConstants} from '../enums/storage.constants';
import {SignupModel} from '../models/signup.model';
import {LoginModel} from '../models/login.model';
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  private TokenData: TokenModel | null = null;

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private http: HttpClient,
              private storageService: StorageService,
              private router: Router) {
  }

  setTokenFromStorage(): void {
    let token = this.storageService.getValue(StorageConstants.TOKEN_KEY)
    console.log(token)
    if (token !== null) {
      this.TokenData = JSON.parse(token)
    }
  }

  getJWTTokenData() {
    return this.TokenData
  }

  login(loginRequestModel: LoginModel): Observable<TokenModel> {
    return this.http.post<TokenModel>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${AuthServiceEndpoints.LOGIN}`, loginRequestModel)
      .pipe(
        map(response => {
          if (response.token) {
            this.TokenData = response
            this.storageService.setItem(StorageConstants.TOKEN_KEY, JSON.stringify(response))
          }
          return response
        }));
  }

  signup(signupRequestModel: SignupModel): Observable<number> {
    return this.http.post<number>(`${this.ENVIRONMENT_TOKEN.serverEndpoint}${AuthServiceEndpoints.SIGNUP}`, signupRequestModel)
  }

  logout() {
    this.storageService.removeItem(StorageConstants.TOKEN_KEY)
    this.router.navigate(["/auth", "login"])
  }
}
