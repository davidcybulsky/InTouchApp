import {IncludeUserModel} from "./include.user.model";

export interface MessageModel {
  id: number
  sender: IncludeUserModel
  recipient: IncludeUserModel
  content: string
  isRead: boolean
}
