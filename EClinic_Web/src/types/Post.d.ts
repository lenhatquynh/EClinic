import { HashTag } from "./Base.type"

export interface Author {
  userID: string
  firstName: string
  lastName: string
  avatar: string
}

export interface IPost {
  id: string
  title: string
  content: string
  image: string[]
  author: Author
  createdAt: string
  updatedAt: string
  likes: number
  isLike: boolean
  isActive: boolean
}
export interface IAnwer {
  id: string
  content: string
  hashTags: HashTag[]
  createdAt: string
  updatedAt: string
  author: Author
  likes: number
  isLike: boolean
}
export interface IComment {
  id: string
  content: string
  author: Author
  createdAt: string
  updatedAt: string
  likeUserIds: any[]
  likes: number
  isLike: boolean
  replyCommentDtos: IComment[]
}
export interface IHashtag {
  hashtagID: string
  hashtagName: string
  count: number
}
export type ICreateCommentForum = {
  postId: string
  content: string
}
export interface CommnentId {
  ParentCommentID?: string | null
  CommentID: string
}
export interface DeleteActionType extends CommnentId {
  kind: ActionComment
}
export interface UpdateActionType extends CommnentId {
  content: string
  kind: ActionComment
}
export interface LikeActionType extends CommnentId {
  kind: ActionComment
}
export type ActionComment = "comment" | "reply"
