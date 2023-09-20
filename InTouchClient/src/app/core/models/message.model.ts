import {IncludeUserModel} from "./include.user.model";

export interface MessageModel {
  sender: IncludeUserModel
  recipient: IncludeUserModel
  content: string
  isRead: boolean
}
