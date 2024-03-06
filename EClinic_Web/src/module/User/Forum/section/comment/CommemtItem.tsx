import useConfirm from "context/ComfirmContext"
import { useState } from "react"
import { toast } from "react-hot-toast"
import {
  DeleteActionType,
  IComment,
  LikeActionType,
  UpdateActionType
} from "types/Post"
import { CommentAvatar } from "./components/Avatar/CommentAvatar"
import { CommentContent } from "./components/Content/CommentContent"
import CommentInput from "./components/Input/CommentInput"

interface Props {
  comment: IComment
  // eslint-disable-next-line no-unused-vars
  onCreateReply: (id: string, content: string) => void
  // eslint-disable-next-line no-unused-vars
  onDeleteComment: (data: DeleteActionType) => void
  // eslint-disable-next-line no-unused-vars
  updateComment: (data: UpdateActionType) => void
  // eslint-disable-next-line no-unused-vars
  onLikeComment: (data: LikeActionType) => void
}
const CommemtItem = ({
  comment,
  onCreateReply,
  onDeleteComment,
  updateComment,
  onLikeComment
}: Props) => {
  const confirm = useConfirm()

  const [openRep, setOpenRep] = useState(false)
  const [valueInput, setValueInput] = useState("")
  const createReply = () => {
    if (valueInput.trim()) {
      onCreateReply(comment.id, valueInput)
      setValueInput("")
      setOpenRep(false)
    } else {
      toast.error("Please enter your reply content")
    }
  }
  const handleDelete = async (data: DeleteActionType) => {
    if (confirm) {
      const choice = await confirm({
        title: "Delete Comment?",
        content: "Are you sure you want to delete this comment?"
      })
      if (choice) {
        onDeleteComment(data)
      }
    }
  }
  return (
    <>
      <article className="p-3 space-y-3 text-base bg-white rounded-lg md:p-6 ">
        <CommentContent
          onReply={() => setOpenRep(!openRep)}
          onLike={() => {
            onLikeComment({
              kind: "comment",
              CommentID: comment.id
            })
          }}
          comment={comment}
          handleDeleteComment={() =>
            handleDelete({
              CommentID: comment.id,
              ParentCommentID: null,
              kind: "reply"
            })
          }
          onEditComment={(value) => {
            if (value.trim()) {
              updateComment({
                content: value,
                CommentID: comment.id,
                kind: "comment",
                ParentCommentID: null
              })
            } else {
              toast.error("Please enter your comment content")
            }
          }}
        />

        <div className="flex flex-col ml-6">
          {comment.replyCommentDtos.map((replyItem) => (
            <article
              className="w-full p-3 text-base bg-white rounded-lg md:p-6"
              key={replyItem.id}
            >
              <CommentContent
                onLike={() => {
                  onLikeComment({
                    kind: "reply",
                    CommentID: replyItem.id,
                    ParentCommentID: comment.id
                  })
                }}
                comment={replyItem}
                handleDeleteComment={() =>
                  handleDelete({
                    CommentID: replyItem.id,
                    ParentCommentID: comment.id,
                    kind: "reply"
                  })
                }
                onEditComment={(value) =>
                  updateComment({
                    content: value,
                    CommentID: replyItem.id,
                    kind: "reply",
                    ParentCommentID: comment.id
                  })
                }
              />
            </article>
          ))}
        </div>
        {openRep && (
          <div className="flex items-center mt-4 ml-4 gap-x-2">
            <CommentAvatar avatar="/images/default.jpeg" />
            <CommentInput
              value={valueInput}
              onChange={(e) => setValueInput(e.target.value)}
              onSubmit={createReply}
            />
          </div>
        )}
      </article>
    </>
  )
}

export default CommemtItem
