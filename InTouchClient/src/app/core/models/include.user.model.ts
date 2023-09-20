import {IncludeUserPhotoModel} from "./include.user.photo.model";

export interface IncludeUserModel {
  id: number
  firstName: string
  lastName: string
  role: string
  userPhoto: IncludeUserPhotoModel
}
