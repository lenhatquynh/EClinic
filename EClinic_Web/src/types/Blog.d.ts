import { Author } from "./Post"
export interface HashTagBlog {
  id: string
  hashtagName: string
  count: number
}
export interface IBlog {
  id: string
  title: string
  content: string
  coverImage?: any
  author: Author
  isActive: boolean
  metaTitle: string
  metaDescription: string
  metaKeywords: string
  hashtags: HashTagBlog[]
  createdAt: string
  updatedAt: string
}
