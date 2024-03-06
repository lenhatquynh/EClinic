import { useMutation, useQuery } from "@tanstack/react-query"
import { QUERY_KEYS } from "shared/constant/constant"
import { DeleteActionType, ICreateCommentForum } from "types/Post"
import { forumService } from "./../../../services/forum.service"
import { CommnentId, UpdateActionType } from "./../../../types/Post.d"

export interface CreatePostForum {
  title: string
  content: string
  images: File[]
}
export interface CreateAnwserPost {
  postId: string
  content: string
  tags: string[]
}
export const useCreatePostMutation = () => {
  return useMutation({
    mutationFn: (data: CreatePostForum) => forumService.createPost(data)
  })
}
export const useGetAllPostForumQuery = (
  pageNumber: number,
  pageSize: number,
  searchText: string
) => {
  const queryKey = [QUERY_KEYS.FORUM.POST, pageNumber, pageSize, searchText]
  return useQuery({
    queryKey,
    queryFn: () => forumService.getAllPost(pageNumber, pageSize, searchText)
  })
}
export const useGetPostsNoActiveQuery = (
  pageNumber: number,
  pageSize: number,
  searchText: string
) => {
  const queryKey = [QUERY_KEYS.FORUM.POST, pageNumber, pageSize, searchText]
  return useQuery({
    queryKey,
    queryFn: () =>
      forumService.getPostsNoActive(pageNumber, pageSize, searchText),
    keepPreviousData: true
  })
}
export const useGetPostForAd = (pageNumber: number, pageSize: number) => {
  const queryKey = [QUERY_KEYS.FORUM.POST, pageNumber, pageSize]
  return useQuery({
    queryKey,
    queryFn: () => forumService.GetPostForAd(pageNumber, pageSize),
    keepPreviousData: true
  })
}
export const useGetPostNoAnserForumQuery = (
  pageNumber: number,
  pageSize: number
) => {
  const queryKey = [QUERY_KEYS.FORUM.POST, pageNumber, pageSize]
  return useQuery({
    queryKey,
    queryFn: () => forumService.getPostNoAnser(pageNumber, pageSize)
  })
}
export const useChangeActivePost = () =>
  useMutation({
    mutationFn: (postId: string) => forumService.changeActivePost(postId)
  })
export const useCreateAwnserPostForumMutation = () =>
  useMutation({
    mutationFn: (body: CreateAnwserPost) => forumService.createAnswer(body)
  })
export const useSearchPostsForum = (
  keyword: string,
  pageNumber: number,
  pageSize: number,
  tags: string[]
) => {
  const queryKey = [
    QUERY_KEYS.FORUM.POST,
    keyword ? keyword : " ",
    tags,
    pageNumber,
    pageSize
  ]
  return useQuery({
    queryKey,
    queryFn: () => forumService.searchPosts(keyword, pageNumber, pageSize, tags)
  })
}
export const useGetPostbyIdQuery = (id: string) => {
  const queryKey = [QUERY_KEYS.FORUM.POST, id]
  return useQuery({
    queryKey,
    queryFn: () => forumService.getPostById(id)
  })
}
export const useDeletePostbyIdMutaiton = () => {
  return useMutation({
    mutationFn: (id: string) => forumService.deletePost(id)
  })
}
export const useGetPostByUserId = (pageNumber: number, pageSize: number) => {
  const queryKey = [QUERY_KEYS.FORUM.POST, pageNumber, pageSize]
  return useQuery({
    queryKey,
    queryFn: () => forumService.getPossByUserId(pageNumber, pageSize)
  })
}
// export const useGetAllCommentForumQuery = (postId: string) => {
//   const queryKey = [QUERY_KEYS.FORUM.COMMENT, postId]
//   return useQuery({
//     queryKey,
//     queryFn: () => forumService.GetAllComment(postId)
//   })
// }
export const useCreateCommentForumMutation = () =>
  useMutation({
    mutationFn: (body: ICreateCommentForum) => forumService.createComment(body)
  })
export const useCreateReplyCommentForumMutation = () => {
  return useMutation({
    mutationFn: (body: ICreateCommentForum) =>
      forumService.createReplyComment(body)
  })
}
export const useDeleteCommnetForumMutation = () => {
  return useMutation({
    mutationFn: (querys: Omit<DeleteActionType, "kind" | "ParentCommentID">) =>
      forumService.deleteCommentByID(querys.CommentID)
  })
}
export const useDeleteReplyCommnetForumMutation = () => {
  return useMutation({
    mutationFn: (querys: Omit<DeleteActionType, "kind">) =>
      forumService.deleteReplyCommentByID(querys)
  })
}
export const useUpdatCommnetForumMutation = () => {
  return useMutation({
    mutationFn: (querys: Omit<UpdateActionType, "kind" | "ParentCommentID">) =>
      forumService.updateComment(querys)
  })
}
export const useUpdateReplyCommnetForumMutation = () => {
  return useMutation({
    mutationFn: (querys: Omit<UpdateActionType, "kind">) =>
      forumService.updateCommentReply(querys)
  })
}
export const useLikeReplyCommnetForumMutation = () => {
  return useMutation({
    mutationFn: (querys: CommnentId) => forumService.likeReplyComment(querys)
  })
}
export const useLikeCommnetForumMutation = () => {
  return useMutation({
    mutationFn: (querys: Omit<CommnentId, "ParentCommentID">) =>
      forumService.likeComment(querys)
  })
}
export const useLikePostForumMutation = () =>
  useMutation({
    mutationFn: (CommnentId: string) => forumService.likePost(CommnentId)
  })
export const useGetAnwerByPostId = (postId: string) => {
  const queryKey = [QUERY_KEYS.FORUM.ANSWER, postId]
  return useQuery({
    queryKey,
    queryFn: () => forumService.getAnwerByPostId(postId)
  })
}
//hashtag

export const useGetAllHashTagQuery = () => {
  const queryKey = [QUERY_KEYS.HASHTAG]
  return useQuery({
    queryKey,
    queryFn: () => forumService.getAllHashtag()
  })
}
export const useGetHashtagBySortQuery = (
  pageNumber: number,
  pageSize: number
) => {
  const queryKey = [QUERY_KEYS.HASHTAG, pageNumber, pageSize]
  return useQuery({
    queryKey,
    queryFn: () => forumService.getTagSortByCount(pageNumber, pageSize)
  })
}
export const useCreatetagMutation = () =>
  useMutation({
    mutationFn: (HashtagName: string) => forumService.createTag(HashtagName)
  })
