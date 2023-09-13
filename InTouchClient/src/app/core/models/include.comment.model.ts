import {IncludeReactionModel} from "./include.reaction.model"
import {IncludeUserModel} from "./include.user.model"

export interface IncludeCommentModel {
  id: number
  userId: number
  author: IncludeUserModel
  content: string
  reactionsData: IncludeReactionModel
}
