import {CreateAddressModel} from "./create.address.model";

export interface CreateUserModel {
  email: string
  password: string
  confirmPassword: string
  firstName: string
  lastName: string
  birthDate: string
  phoneNumber: string
  description: string

  role: string

  address: CreateAddressModel
}
