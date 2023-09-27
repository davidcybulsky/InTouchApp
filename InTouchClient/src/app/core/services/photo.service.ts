import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ENVIRONMENT_TOKEN} from "../tokens/environment.token";
import {IEnvoronment} from "../../../environment/environment.interface";
import {PhotoServiceEndpoints} from "../enums/photo.service.endpoints";
import {IncludePhotoModel} from "../models/include.photo.model";
import {formatDate} from "@angular/common";

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  constructor(@Inject(ENVIRONMENT_TOKEN) private ENVIRONMENT_TOKEN: IEnvoronment,
              private httpClient: HttpClient) { }

  addUserPhoto(data: File) {
    let file = new FormData();
    file.append("file", data, data.name)
    //const headers = new HttpHeaders({ 'enctype': 'multipart/form-data' });

    console.log(file.get('file'))
    return this.httpClient.post<IncludePhotoModel>(`${this.ENVIRONMENT_TOKEN.apiUrl}${PhotoServiceEndpoints.ADD_USER_PHOTO}`, file, {headers: new HttpHeaders({ 'content-type': 'multipart/form-data' })})
  }

  setMainUserPhoto(photoId: number) {
    return this.httpClient.put(`${this.ENVIRONMENT_TOKEN.apiUrl}${PhotoServiceEndpoints.SET_AS_MAIN_USER_PHOTO}${photoId}`,null)
  }

  deleteUserPhoto(photoId: number) {
    return this.httpClient.delete(`${this.ENVIRONMENT_TOKEN.apiUrl}${PhotoServiceEndpoints.DELETE_USER_PHOTO}${photoId}`)
  }
}
