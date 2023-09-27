import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ENVIRONMENT_TOKEN} from "../tokens/environment.token";
import {IEnvoronment} from "../../../environment/environment.interface";
import {PhotoServiceEndpoints} from "../enums/photo.service.endpoints";

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private httpClient: HttpClient) { }

  setMainUserPhoto(photoId: number) {
    return this.httpClient.put(`${this.ENVIRONMENT_TOKEN.apiUrl}${PhotoServiceEndpoints.SET_AS_MAIN_USER_PHOTO}${photoId}`,null)
  }

  deleteUserPhoto(photoId: number) {
    return this.httpClient.delete(`${this.ENVIRONMENT_TOKEN.apiUrl}${PhotoServiceEndpoints.DELETE_USER_PHOTO}${photoId}`)
  }
}
