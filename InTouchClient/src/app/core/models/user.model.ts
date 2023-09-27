import {AddressModel} from "./address.model"
import {PostModel} from "./post.model"
import {IncludePhotoModel} from "./include.photo.model";

export interface UserModel {
  id: number
  email: string
  firstName: string
  lastName: string
  birthDate: string
  phoneNumber: string
  description: string

  role: string

  facebookURL: string
  instagramURL: string
  linkedInURL: string
  tikTokURL: string
  youTubeURL: string
  twitterURL: string

  address: AddressModel
  posts: PostModel[]
  userPhotos: IncludePhotoModel []
}
