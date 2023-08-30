import { IncludeUserModel } from "./include.user.model"

export interface IncludeReactionModel {
    id: number
    reactionType: string
    userId: number
    author: IncludeUserModel
}