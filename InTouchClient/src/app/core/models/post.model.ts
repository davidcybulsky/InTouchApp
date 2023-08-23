import { IncludeUserModel } from "./include.user.model"

export interface PostModel {
    id: number,
    title: string,
    content: string,
    authorId: number
    author: IncludeUserModel
}