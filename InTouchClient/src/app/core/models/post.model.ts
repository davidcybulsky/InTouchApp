import {IncludeCommentModel} from "./include.comment.model"
import {IncludeReactionModel} from "./include.reaction.model"
import {IncludeUserModel} from "./include.user.model"

export interface PostModel {
  id: number,
  title: string,
  content: string,
  authorId: number
  author: IncludeUserModel
  comments: IncludeCommentModel[]
  reactionsData: IncludeReactionModel
}
