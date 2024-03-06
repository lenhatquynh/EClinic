import Popover from "components/User/Popover"
import { ToastNavigate } from "module/User/components/Toast/ToastNavigate"
import { useRouter } from "next/router"
import { useState } from "react"
import { toast } from "react-hot-toast"
import { AiOutlineLike, AiTwotoneLike } from "react-icons/ai"
import { HiOutlineDotsHorizontal } from "react-icons/hi"
import { useSelector } from "react-redux"
import { routers } from "shared/constant/routers"
import { combineName, dayformat } from "shared/helpers/helper"
import { RootState } from "store/store"
import { IComment } from "types/Post"
import { CommentAvatar } from "../Avatar/CommentAvatar"
import CommentInput from "../Input/CommentInput"
type Props = {
  comment: IComment
  handleDeleteComment: () => void
  // eslint-disable-next-line no-unused-vars
  onEditComment: (value: string) => void
  onLike: () => void
  onReply?: () => void
}
export const CommentContent = ({
  comment,
  onLike,
  onReply,
  handleDeleteComment,
  onEditComment
}: Props) => {
  const router = useRouter()
  const [isEdit, setIsEdit] = useState(false)
  const [inputValue, setInputValue] = useState(comment.content)
  const auth = useSelector((state: RootState) => state.auth)
  const handleLike = () => {
    if (auth.user.userId) {
      onLike()
    } else {
      toast((t) => (
        <ToastNavigate
          url={routers.signIn}
          t={t}
          labelButton="Login"
          router={router}
          title="Please login to like comment"
        />
      ))
    }
  }
  return (
    <>
      <div className="flex items-center justify-between mb-2">
        <div className="flex items-center">
          <div className="inline-flex items-center mr-3 text-sm text-gray-900 ">
            <CommentAvatar avatar={comment.author.avatar} />
            <span>
              {combineName(comment.author.firstName, comment.author.lastName)}
            </span>
          </div>
          <p className="text-sm text-gray-600 ">
            <time dateTime={dayformat(comment.updatedAt)}>
              {dayformat(comment.updatedAt)}
            </time>
          </p>
        </div>
        {comment.author.userID === auth.user.userId && (
          <Popover
            position="left"
            className="w-fit"
            buttonTrigger={
              <button
                className="inline-flex items-center p-2 text-sm font-medium text-center text-gray-400 bg-white border-none rounded-lg cursor-pointer hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-gray-50"
                type="button"
              >
                <HiOutlineDotsHorizontal className="text-xl" />
              </button>
            }
          >
            <ul className="py-2 bg-white rounded-md shadow">
              <li
                className="px-4 py-2 text-sm text-gray-500 transition-all cursor-pointer hover:bg-gray-200"
                onClick={() => setIsEdit(!isEdit)}
              >
                Edit
              </li>
              <li
                className="px-4 py-2 text-sm text-red-500 transition-all cursor-pointer hover:bg-gray-200"
                onClick={() => handleDeleteComment()}
              >
                Delete
              </li>
            </ul>
          </Popover>
        )}
      </div>
      {isEdit ? (
        <div className="flex items-center mt-4 gap-x-2">
          <CommentInput
            value={inputValue}
            onChange={(e) => setInputValue(e.target.value)}
            onSubmit={() => {
              onEditComment(inputValue)
              setIsEdit(false)
            }}
          />
        </div>
      ) : (
        <p className="text-sm font-light leading-normal text-gray-500 md:text-base">
          {comment.content}
        </p>
      )}
      <div className="flex items-center mt-4 space-x-4">
        {comment.author.userID !== auth.user.userId && (
          <button
            onClick={handleLike}
            className="flex items-center space-x-1 text-sm text-gray-500 bg-transparent border-none outline-none cursor-pointer hover:underline"
            type="button"
          >
            {comment.isLike ? (
              <AiTwotoneLike className="text-primary " />
            ) : (
              <AiOutlineLike />
            )}
            <span>{comment.likes} Likes</span>
          </button>
        )}

        {onReply && auth.user.userId && (
          <button
            onClick={onReply}
            type="button"
            className="flex items-center text-sm text-gray-500 bg-transparent border-none outline-none cursor-pointer hover:underline"
          >
            <svg
              aria-hidden="true"
              className="w-4 h-4 mr-1"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M8 12h.01M12 12h.01M16 12h.01M21 12c0 4.418-4.03 8-9 8a9.863 9.863 0 01-4.255-.949L3 20l1.395-3.72C3.512 15.042 3 13.574 3 12c0-4.418 4.03-8 9-8s9 3.582 9 8z"
              />
            </svg>
            <span>Reply</span>
          </button>
        )}
      </div>
    </>
  )
}
