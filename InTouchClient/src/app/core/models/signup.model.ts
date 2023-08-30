import { CreateAddressModel } from "./create.address.model"

export interface SignupModel {
    email: string
    password: string
    confirmPassword: string
    firstName: string
    lastName: string
    birthDate: string
    phoneNumber: string
    description: string
    
    address: CreateAddressModel
}