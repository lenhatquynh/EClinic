import { IconButton } from "@mui/material"
import { DetailedHTMLProps, InputHTMLAttributes } from "react"
import { AiOutlineSend } from "react-icons/ai"
interface Props
  extends DetailedHTMLProps<
    InputHTMLAttributes<HTMLInputElement>,
    HTMLInputElement
  > {
  onSubmit: () => void
}
const CommentInput = ({ onSubmit, ...props }: Props) => {
  return (
    <>
      <input
        onKeyDown={(e) => {
          if (e.key === "Enter") {
            onSubmit()
          }
        }}
        {...props}
        type="text"
        placeholder="Write your reply comment here"
        className="w-full px-4 py-1.5 bg-white border border-gray-200 border-solid rounded-xl text-sm"
      />
      <IconButton size="small" onClick={onSubmit}>
        <AiOutlineSend />
      </IconButton>
    </>
  )
}

export default CommentInput
