import {IncludePhotoModel} from "./include.photo.model";

export interface IncludeUserModel {
  id: number
  firstName: string
  lastName: string
  role: string
  userPhoto: IncludePhotoModel
}
