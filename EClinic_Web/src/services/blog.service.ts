import { AxiosResponse } from "axios"
import { CreatePostBlog, UpdatePostBlog } from "hooks/query/blog/useBlog"
import axiosClient from "shared/axios/httpClient"
import { URL_API } from "shared/constant/constant"
import { HashTagBlog, IBlog } from "types/Blog"
import { IServerResponse } from "types/server/IServerResponse"
class BlogService {
  async searchPosts(
    keyword: string,
    pageNumber: number,
    pageSize: number,
    tags: string[]
  ) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.BLOG_POST}/SearchBlog?SearchText=${keyword}&${
          tags.length > 0 ? tags.map((item) => `Tags=${item}`).join("&") : ""
        }`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<IBlog[]>>
    } catch (error) {
      console.log("BlogService ~ error:", error)
    }
  }
  async searchBlogForAd(
    keyword: string,
    pageNumber: number,
    pageSize: number,
    tags: string[]
  ) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.BLOG_POST}/SearchBlogForAd?SearchText=${keyword}&${
          tags.length > 0 ? tags.map((item) => `Tags=${item}`).join("&") : ""
        }`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<IBlog[]>>
    } catch (error) {
      console.log("BlogService ~ error:", error)
    }
  }
  async getAllBlog(pageNumber: number, pageSize: number) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.BLOG_POST}/GetAllBlog`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<IBlog[]>>
    } catch (error) {
      console.log("BlogService ~ error:", error)
    }
  }
  async getPostById(id: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.BLOG_POST}/GetBlogByID?BlogID=${id}`
    )
    return res.data as IServerResponse<IBlog>
  }
  async getPostByIdForAd(id: string) {
    const res: AxiosResponse = await axiosClient.get(
      `${URL_API.BLOG_POST}/GetBlogByIDForAd?BlogID=${id}`
    )
    return res.data as IServerResponse<IBlog>
  }
  async createPost(data: CreatePostBlog) {
    try {
      const formData = new FormData()
      for (const [key, value] of Object.entries(data)) {
        if (key === "hashtagId") {
          value.forEach((hash: string) => {
            formData.append(key, hash)
          })
        } else {
          formData.append(key, value)
        }
      }
      const res: AxiosResponse = await axiosClient.post(
        `${URL_API.BLOG_POST}/CreateBlog`,
        formData,
        {
          headers: { "content-type": "multipart/form-data" }
        }
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("BlogService ~ createPost ~ error:", error)
    }
  }
  async updatePost(data: UpdatePostBlog) {
    try {
      const formData = new FormData()
      for (const [key, value] of Object.entries(data)) {
        if (key === "hashtagId") {
          value.forEach((hash: string) => {
            formData.append(key, hash)
          })
        } else {
          formData.append(key, value)
        }
      }
      const res: AxiosResponse = await axiosClient.put(
        `${URL_API.BLOG_POST}/UpdateBlog`,
        formData,
        {
          headers: { "content-type": "multipart/form-data" }
        }
      )
      return res.data as IServerResponse<string>
    } catch (error) {
      console.log("BlogService ~ UpdatePost ~ error:", error)
    }
  }
  async getAllHashTag() {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.BLOG_HASHTAG}/getAllHashTag`
      )
      return res as AxiosResponse<IServerResponse<HashTagBlog[]>>
    } catch (error) {
      console.log("BlogService ~ error:", error)
    }
  }
  async createHashtag(hashTag: string) {
    const res: AxiosResponse = await axiosClient.post(
      `${URL_API.BLOG_HASHTAG}/CreateHashtag?HashtagName=${hashTag}`,
      {
        headers: { "content-type": "multipart/form-data" }
      }
    )
    return res.data as IServerResponse<string>
  }
  async getTagSortByCount(pageNumber: number, pageSize: number) {
    try {
      const res: AxiosResponse = await axiosClient.get(
        `${URL_API.BLOG_HASHTAG}/GetTagSortByCount`,
        {
          headers: {
            PageNumber: pageNumber,
            PageSize: pageSize
          }
        }
      )
      return res as AxiosResponse<IServerResponse<HashTagBlog[]>>
    } catch (error) {
      console.log("getTagSortByCount ~ error:", error)
    }
  }
  async upLoadImage(file: File) {
    try {
      const formData = new FormData()
      formData.append("image", file)
      const res: AxiosResponse = await axiosClient.post(
        `${URL_API.BLOG_POST}/UploadImage`,
        formData,
        {
          headers: { "content-type": "multipart/form-data" }
        }
      )
      return res.data as IServerResponse<null>
    } catch (error) {
      console.log("BlogService ~ upLoadImage ~ error:", error)
    }
  }
}
export const blogService = new BlogService()
