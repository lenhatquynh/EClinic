import { useQueryClient } from "@tanstack/react-query"
import Favorite from "components/Common/Favorite"
import ImageCustom from "components/Common/ImageCustom"
import {
  useCreateCommentForumMutation,
  useCreateReplyCommentForumMutation,
  useDeleteCommnetForumMutation,
  useDeleteReplyCommnetForumMutation,
  useLikeCommnetForumMutation,
  useLikePostForumMutation,
  useLikeReplyCommnetForumMutation,
  useUpdatCommnetForumMutation,
  useUpdateReplyCommnetForumMutation
} from "hooks/query/forum/useForum"
import dynamic from "next/dynamic"
import { toast } from "react-hot-toast"
import { useTranslation } from "react-i18next"
import { QUERY_KEYS } from "shared/constant/constant"
import {
  DeleteActionType,
  IPost,
  LikeActionType,
  UpdateActionType
} from "types/Post"
import AnswerQuestion from "../answer/AnswerQuestion"
import { queryClient } from "pages/_app"

const Comment = dynamic(
  () => import("../comment").then((module) => module.default),
  {
    ssr: false
  }
)
interface Props {
  post: IPost
}
const DetailForum = ({ post }: Props) => {
  const { t } = useTranslation(["base", "forum"])
  // const { data, isLoading, isError, refetch } = useGetAllCommentForumQuery(
  //   post.id
  // )

  const createCommentForumMutation = useCreateCommentForumMutation()
  const createReplyCommentForumMutation = useCreateReplyCommentForumMutation()
  const deleteReplyCommnetForumMutation = useDeleteReplyCommnetForumMutation()
  const deleteCommnetForumMutation = useDeleteCommnetForumMutation()
  const updatCommnetForumMutation = useUpdatCommnetForumMutation()
  const updateReplyCommnetForumMutation = useUpdateReplyCommnetForumMutation()
  const likeReplyCommnetForumMutation = useLikeReplyCommnetForumMutation()
  const likeCommnetForumMutation = useLikeCommnetForumMutation()
  const likePostForumMutation = useLikePostForumMutation()

  const handleCreateComment = (value: string) => {
    if (value) {
      createCommentForumMutation.mutate(
        {
          postId: post.id,
          content: value
        },
        {
          onSuccess: () => {
            queryClient.refetchQueries([QUERY_KEYS.FORUM.COMMENT])
            toast.success("Comment sucessfully!")
          },
          onError: () => {
            toast.error("Create comment failed!")
          }
        }
      )
    } else {
      toast.error("Please enter your content of comment")
    }
  }
  const handleCreateReply = (id: string, value: string) => {
    createReplyCommentForumMutation.mutate(
      {
        postId: id,
        content: value
      },
      {
        onSuccess: () => {
          queryClient.refetchQueries([QUERY_KEYS.FORUM.COMMENT])
          toast.success("Reply Comment sucessfully!")
        },
        onError: () => {
          toast.error("Create comment failed!")
        }
      }
    )
  }
  const handleDeleteComment = (data: DeleteActionType) => {
    if (data.ParentCommentID) {
      deleteReplyCommnetForumMutation.mutate(data, {
        onSuccess: () => {
          queryClient.refetchQueries([QUERY_KEYS.FORUM.COMMENT])
          toast.success("Delete Comment sucessfully!")
        },
        onError: () => {
          toast.error("Delete comment failed!")
        }
      })
    } else {
      deleteCommnetForumMutation.mutate(data, {
        onSuccess: () => {
          queryClient.refetchQueries([QUERY_KEYS.FORUM.COMMENT])
          toast.success("Delete Comment sucessfully!")
        },
        onError: () => {
          toast.error("Delete comment failed!")
        }
      })
    }
  }
  const handleUpdateComment = (data: UpdateActionType) => {
    if (data.kind === "comment") {
      updatCommnetForumMutation.mutate(
        {
          ...data
        },
        {
          onSuccess: () => {
            queryClient.refetchQueries([QUERY_KEYS.FORUM.COMMENT])
            toast.success("Update Comment sucessfully!")
          },
          onError: () => {
            toast.error("Update comment failed!")
          }
        }
      )
    } else if (data.kind === "reply") {
      updateReplyCommnetForumMutation.mutate(
        {
          ...data
        },
        {
          onSuccess: () => {
            queryClient.refetchQueries([QUERY_KEYS.FORUM.COMMENT])
            toast.success("Update Comment sucessfully!")
          },
          onError: () => {
            toast.error("Update comment failed!")
          }
        }
      )
    }
  }
  const handleLikeComment = (data: LikeActionType) => {
    if (data.kind === "comment") {
      likeCommnetForumMutation.mutate(
        { ...data },
        {
          onSuccess: () => {
            queryClient.refetchQueries([QUERY_KEYS.FORUM.COMMENT])
          },
          onError: () => {
            toast.error("Like comment failed!")
          }
        }
      )
    } else if (data.kind === "reply") {
      likeReplyCommnetForumMutation.mutate(
        { ...data },
        {
          onSuccess: () => {
            queryClient.refetchQueries([QUERY_KEYS.FORUM.COMMENT])
          },
          onError: () => {
            toast.error("Like comment failed!")
          }
        }
      )
    }
  }
  const handleLikePost = (isLike: boolean) =>
    likePostForumMutation.mutate(post.id, {
      onSuccess: () => {
        queryClient.invalidateQueries([QUERY_KEYS.FORUM.POST, post.id])
        queryClient.invalidateQueries([QUERY_KEYS.FORUM.POST])
        if (!isLike) toast.success("Like post successfully!")
        else toast.success("UnLike post successfully!")
      },
      onError: () => {
        toast.error("Like post failed!")
      }
    })

  return (
    <>
      <div className="flex flex-col justify-start gap-y-4 background-primary">
        <h3 className="text-xl">{post.title}</h3>
        <div className="relative w-full overflow-hidden rounded-sm h-96">
          <ImageCustom
            src={post.image[0] || "/images/sample.png"}
            fill
            alt="image"
            className="object-cover"
          />
        </div>
        <div className="flex flex-col space-y-2">
          <div className="flex justify-between">
            <h4 className="text-lg">{t("forum:question")}</h4>
            <Favorite
              isFavorite={post.isLike}
              onClick={() => handleLikePost(post.isLike)}
              content={`${post.likes} Cảm ơn`}
            />
          </div>
          <p className="text-[#696974] text-sm leading-loose">{post.content}</p>
        </div>
        <div className="w-full h-[1px] bg-slate-200 my-3"></div>
        <AnswerQuestion postId={post.id} />
        {post.id && (
          <Comment
            postId={post.id}
            onCreateComment={handleCreateComment}
            onCreateReply={handleCreateReply}
            onDeleteComment={handleDeleteComment}
            updateComment={handleUpdateComment}
            onLikeComment={handleLikeComment}
          />
        )}
      </div>
      {/* <div className="col-span-3 space-y-4 md:col-span-2 background-primary">
        <ListCardForum title={t("forum:related")} />
      </div> */}
    </>
  )
}

export default DetailForum
