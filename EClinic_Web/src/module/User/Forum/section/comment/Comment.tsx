import { useInfiniteQuery } from "@tanstack/react-query"
import CustomButton from "components/User/Button"
import { ToastNavigate } from "module/User/components/Toast/ToastNavigate"
import { useRouter } from "next/router"
import { useRef } from "react"
import { toast } from "react-hot-toast"
import { useTranslation } from "react-i18next"
import { useSelector } from "react-redux"
import { forumService } from "services/forum.service"
import { PAGE_SIZE, QUERY_KEYS } from "shared/constant/constant"
import { routers } from "shared/constant/routers"
import { getDataPaginate } from "shared/helpers/helper"
import { RootState } from "store/store"
import { DeleteActionType, LikeActionType, UpdateActionType } from "types/Post"
import CommemtItem from "./CommemtItem"
import classNames from "classnames"
interface Props {
  postId: string
  // eslint-disable-next-line no-unused-vars
  onCreateComment: (value: string) => void
  // eslint-disable-next-line no-unused-vars
  onCreateReply: (id: string, content: string) => void
  // eslint-disable-next-line no-unused-vars
  onDeleteComment: (data: DeleteActionType) => void
  // eslint-disable-next-line no-unused-vars
  updateComment: (data: UpdateActionType) => void
  // eslint-disable-next-line no-unused-vars
  onLikeComment: (data: LikeActionType) => void
}
const Comment = ({
  postId,
  onCreateComment,
  onCreateReply,
  onDeleteComment,
  onLikeComment,
  updateComment
}: Props) => {
  const router = useRouter()
  const inputRef = useRef<HTMLTextAreaElement>(null)
  const { t } = useTranslation(["base", "forum"])
  const auth = useSelector((state: RootState) => state.auth)

  const { data, isLoading, hasNextPage, fetchNextPage } = useInfiniteQuery(
    [QUERY_KEYS.FORUM.COMMENT],
    async ({ pageParam = 1 }) => {
      const res = await forumService.GetAllComment(postId, pageParam, PAGE_SIZE)
      return res
    },
    {
      getNextPageParam: (lastPage) =>
        getDataPaginate(lastPage).HasNext
          ? getDataPaginate(lastPage).PageIndex + 1
          : undefined
    }
  )
  const handleSubmitComtent = () => {
    if (auth.user.userId) {
      if (inputRef.current) {
        if (inputRef.current?.value.trim()) {
          onCreateComment(inputRef.current?.value || "")
          inputRef.current!.value = ""
        } else {
          toast.error("Please enter your comment content")
        }
      }
    } else {
      toast((t) => (
        <ToastNavigate
          url={routers.signIn}
          t={t}
          labelButton="Login"
          router={router}
          title="Please login to write comment"
        />
      ))
    }
  }
  if (isLoading) {
    return <div>Loading...</div>
  }
  return (
    <>
      <section className="py-4 bg-white md:py-6 ">
        <div className="w-full mx-auto">
          <div className="flex items-center justify-between mb-6">
            <h2 className="text-lg font-bold text-gray-900 lg:text-2xl ">
              {/* {t("forum:discussion")} ({comments.length}) */}
            </h2>
          </div>
          <form className="mb-6">
            <div className="w-full px-4 py-2 mb-4 bg-white border border-gray-200 border-solid rounded-lg rounded-t-lg ">
              <textarea
                onKeyDown={(e) => {
                  if (e.key === "Enter") {
                    handleSubmitComtent()
                  }
                }}
                ref={inputRef}
                id="comment"
                rows={6}
                className="w-full px-0 text-sm text-gray-900 border-0 focus:ring-0 focus:outline-none"
                placeholder={t("forum:writeComment")}
                required
                defaultValue={""}
              />
            </div>
            <CustomButton
              kind="primary"
              size="small"
              className="!h-9"
              onClick={() => handleSubmitComtent()}
            >
              <span className="!text-[12px]">{t("forum:btnComment")}</span>
            </CustomButton>
          </form>
          {data?.pages &&
            data?.pages.map(
              (page) =>
                page.data.data?.length > 0 &&
                page.data.data.map((item) => {
                  return (
                    <CommemtItem
                      key={item.id}
                      comment={item}
                      onCreateReply={onCreateReply}
                      onDeleteComment={onDeleteComment}
                      updateComment={updateComment}
                      onLikeComment={onLikeComment}
                    />
                  )
                })
            )}
          <CustomButton
            onClick={() => {
              hasNextPage && fetchNextPage()
            }}
            className={classNames("mx-auto", !hasNextPage && "hidden")}
          >
            Load more
          </CustomButton>
          {/* {comments.length === 0 && <EmtyData message="No comment yet" />} */}
        </div>
      </section>
    </>
  )
}

export default Comment
