import { PostModel } from "./post.model"

export interface UserModel {
    facebookURL: string
    instagramURL: string 
    linkedInURL: string 
    tikTokURL: string 
    youTubeURL: string 
    twitterURL: string 
    posts: PostModel[]
}