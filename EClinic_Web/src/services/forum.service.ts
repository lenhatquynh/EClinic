import { AxiosResponse } from "axios"
import { CreateAnwserPost, CreatePostForum } from "hooks/query/forum/useForum"
import axiosClient from "shared/axios/httpClient"
import axiosServer from "shared/axios/httpSever"
import { URL_API } from "shared/constant/constant"
import { IComment, IHashtag, IPost } from "types/Post"
import { IServerResponse } from "types/server/IServerResponse"
import {
  CommnentId,
  DeleteActionType,
  IAnwer,
  ICreateCommentForum,
  UpdateActionType
} from "./../types/Post.d"
class ForumService {
  async searchPosts(
    keyword: string,
    pageNumber: number,
    pageSize: number,
    tags: string[]
  ) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.FORUM_POST}/SearchPost?SearchText=${keyword}&${
        tags.length > 0 && tags.map((item) => `Tags=${item}`).join("&")
      }`,
      {
        headers: {
          PageNumber: pageNumber,
          PageSize: pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<IPost[]>>
  }
  async createPost(data: CreatePostForum) {
    const formData = new FormData()
    formData.append("title", data.title)
    formData.append("content", data.content)
    data.images.forEach((item) => {
      formData.append("images", item)
    })
    const res: AxiosResponse = await axiosClient.post(
      `${URL_API.FORUM_POST}/CreatePost`,
      formData,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<string>
  }
  async createAnswer(data: CreateAnwserPost) {
    const res: AxiosResponse = await axiosClient.post(
      `${URL_API.FORUM_POST_ANWERS}/CreateAnswer`,
      { ...data }
    )
    return res.data as IServerResponse<string>
  }
  async getAllPost(pageNumber: number, pageSize: number, searchText: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.FORUM_POST}/GetAllPost`,
      {
        params: {
          searchText
        },
        headers: {
          PageNumber: pageNumber,
          PageSize: pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<IPost[]>>
  }
  async getPostNoAnser(pageNumber: number, pageSize: number) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.FORUM_POST}/GetPostNoAnswer`,
      {
        headers: {
          PageNumber: pageNumber,
          PageSize: pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<IPost[]>>
  }
  async getPossByUserId(pageNumber: number, pageSize: number) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.FORUM_POST}/GetPostOfUser`,
      {
        headers: {
          PageNumber: pageNumber,
          PageSize: pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<IPost[]>>
  }
  async changeActivePost(postId: string) {
    const res: AxiosResponse = await axiosClient.put(
      `${URL_API.FORUM_POST}/ChangeActivePost?PostID=${postId}`
    )
    return res.data as IServerResponse<null>
  }
  async deletePostByID(postId: string) {
    const res: AxiosResponse = await axiosClient.delete(
      `${URL_API.FORUM_POST}/DeletePostByID?PostID=${postId}`
    )
    return res.data as IServerResponse<string>
  }
  async getPostById(id: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.FORUM_POST}/GetPostById?PostID=${id}`
    )
    return res.data as IServerResponse<IPost>
  }
  async GetAllComment(postId: string, pageNumber: number, pageSize: number) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.FORUM_POST_COMMENT}/GetAllComment?PostID=${postId}`,
      {
        headers: {
          PageNumber: pageNumber,
          PageSize: pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<IComment[]>>
  }
  async deletePost(postId: string) {
    const res: AxiosResponse = await axiosClient.delete(
      `${URL_API.FORUM_POST}/DeletePostByID?PostID=${postId}`
    )
    return res.data as IServerResponse<null>
  }
  async createComment(body: ICreateCommentForum) {
    const res: AxiosResponse = await axiosClient.post(
      `${URL_API.FORUM_POST_COMMENT}/CreateComment`,
      { ...body }
    )
    return res.data as IServerResponse<null>
  }
  async createReplyComment(body: ICreateCommentForum) {
    const res: AxiosResponse = await axiosClient.post(
      `${URL_API.FORUM_POST_COMMENT}/CreateReplyComment`,
      {
        parentCommentID: body.postId,
        ...body
      }
    )
    return res.data as IServerResponse<null>
  }
  async deleteCommentByID(commentID: string) {
    const res: AxiosResponse = await axiosClient.delete(
      `${URL_API.FORUM_POST_COMMENT}/DeleteCommentByID?CommentID=${commentID}`
    )
    return res.data as IServerResponse<null>
  }
  async deleteReplyCommentByID(value: Omit<DeleteActionType, "kind">) {
    const res: AxiosResponse = await axiosClient.delete(
      `${URL_API.FORUM_POST_COMMENT}/DeleteReplyCommentByID?CommentID=${value.CommentID}&ParentCommentID=${value.ParentCommentID}`
    )
    return res.data as IServerResponse<null>
  }
  async updateComment(
    value: Omit<UpdateActionType, "kind" | "ParentCommentID">
  ) {
    const res: AxiosResponse = await axiosClient.put(
      `${URL_API.FORUM_POST_COMMENT}/UpdateComment`,
      {
        ...value
      }
    )
    return res.data as IServerResponse<null>
  }
  async updateCommentReply(value: Omit<UpdateActionType, "kind">) {
    const res: AxiosResponse = await axiosClient.put(
      `${URL_API.FORUM_POST_COMMENT}/UpdateReplyComment`,
      {
        ...value
      }
    )
    return res.data as IServerResponse<null>
  }
  async likeComment(value: Omit<CommnentId, "ParentCommentID">) {
    const res: AxiosResponse = await axiosClient.put(
      `${URL_API.FORUM_POST_COMMENT}/LikeComment?CommentID=${value.CommentID}`
    )
    return res.data as IServerResponse<null>
  }
  async likeReplyComment(value: CommnentId) {
    const res: AxiosResponse = await axiosClient.put(
      `${URL_API.FORUM_POST_COMMENT}/LikeReplyComment?CommentID=${value.CommentID}&ParentCommentID=${value.ParentCommentID}`
    )
    return res.data as IServerResponse<null>
  }
  async likePost(postId: string) {
    const res: AxiosResponse = await axiosClient.put(
      `${URL_API.FORUM_POST}/LikePost?PostID=${postId}`
    )
    return res.data as IServerResponse<null>
  }
  async getAnwerByPostId(postId: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.FORUM_POST_ANWERS}/GetAnswerByID?PostID=${postId}`
    )
    return res.data as IServerResponse<IAnwer>
  }
  async getAllHashtag() {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.FORUM_POST_HASHTAG}/GetAllHashtag`
    )
    return res.data as IServerResponse<IHashtag[]>
  }
  async getTagSortByCount(pageNumber: number, pageSize: number) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.FORUM_POST_HASHTAG}/GetTagSortByCount`,
      {
        headers: {
          PageNumber: pageNumber,
          PageSize: pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<IHashtag[]>>
  }
  async createTag(HashtagName: string) {
    const res: AxiosResponse = await axiosClient.post(
      `${URL_API.FORUM_POST_HASHTAG}/CreateHashtag?HashtagName=${HashtagName}`
    )
    return res.data as IServerResponse<null>
  }
  async getPostsNoActive(
    pageNumber: number,
    pageSize: number,
    searchText: string
  ) {
    const res: AxiosResponse = await axiosServer.get(
      `${URL_API.FORUM_POST}/GetPostNotActive?SearchText=${searchText}`,
      {
        headers: {
          PageNumber: pageNumber,
          PageSize: pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<IPost[]>>
  }
  async GetPostForAd(pageNumber: number, pageSize: number) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.FORUM_POST}/GetPostForAd`,
      {
        headers: {
          PageNumber: pageNumber,
          PageSize: pageSize
        }
      }
    )
    return res as AxiosResponse<IServerResponse<IPost[]>>
  }
}
export const forumService = new ForumService()
