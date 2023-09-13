import {UpdateAddressModel} from "./update.address.model"

export interface UpdateAccountModel {
  firstName: string
  lastName: string
  phoneNumber: string
  description: string

  facebookURL: string
  instagramURL: string
  linkedInURL: string
  tikTokURL: string
  youTubeURL: string
  twitterURL: string

  address: UpdateAddressModel
}
