import { AddressModel } from "./address.model";

export interface UpdateUserModel {
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
}