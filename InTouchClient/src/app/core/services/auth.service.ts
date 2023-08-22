import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequestModel } from '../models/login.request.model';
import { LoginResponseModel } from '../models/login.response.model';
import { ENVIRONMENT_TOKEN } from '../tokens/environment.token';
import { IEnvoronment } from 'src/environment/environment.interface';
import { AuthServiceEndpoints } from '../enums/auth.service.endpoints';
import { map } from 'rxjs';
import { StorageService } from './storage.service';
import { StorageConstants } from '../enums/storage.constants';

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN : IEnvoronment, 
              private http: HttpClient,
              private storageService: StorageService) { }

  login(loginRequestModel: LoginRequestModel) {
    return this.http.post<LoginResponseModel>(`${ this.ENVIRONMENT_TOKEN.serverEndpoint }${AuthServiceEndpoints.LOGIN}`, loginRequestModel)
      .pipe(
      map(response => {
        if(response.token){
          this.storageService.setItem(StorageConstants.TOKEN_KEY, response.token)
        }
      }));
  }
}
