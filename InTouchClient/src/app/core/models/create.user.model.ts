import { AddressModel } from "./address.model";

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
    
    address: AddressModel
}