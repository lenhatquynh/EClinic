import { IconButton, Tooltip } from "@mui/material"
import classNames from "classnames"
import ModalPrimary from "components/Common/Modal/ModalPrimary"
import { UpdateCover } from "components/Common/UpLoadImage"
import CustomButton from "components/User/Button"
import { useCreateChatMessageFile } from "hooks/query/chat/message"
import { useRouter } from "next/router"
import { useState } from "react"
import { toast } from "react-hot-toast"
import { HiOutlinePaperAirplane } from "react-icons/hi2"
interface Props {
  // eslint-disable-next-line no-unused-vars
  onCreate: (value: string) => void
  isLoading: boolean
}
const InputMessage = ({ onCreate, isLoading = false }: Props) => {
  const [textInput, setTextInput] = useState("")

  const { query } = useRouter()
  const createImageMutation = useCreateChatMessageFile()
  const [show, setShow] = useState(false)
  const [file, setFile] = useState<any>(null)
  const onFileChange = (file: File | null) => {
    setFile(file)
  }
  const handleUploadImage = () => {
    if (!file) {
      toast.error("Please choose image")
    } else {
      createImageMutation.mutate(
        {
          roomId: query.roomId as string,
          file: file
        },
        {
          onSuccess: () => {
            toast.success("Upload image success")
            setShow(false)
          },
          onError: () => {
            toast.error("Upload image error")
          }
        }
      )
    }
  }
  const handleSendMessage = () => {
    onCreate(textInput)
    setTextInput("")
  }
  return (
    <div className="flex items-end w-full px-5 py-4 bg-white gap-x-2 ">
      <Tooltip title="Update image" placement="top">
        <IconButton size="medium" onClick={() => setShow(true)}>
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth={1.5}
            stroke="currentColor"
            className="w-6 h-6"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M13.19 8.688a4.5 4.5 0 011.242 7.244l-4.5 4.5a4.5 4.5 0 01-6.364-6.364l1.757-1.757m13.35-.622l1.757-1.757a4.5 4.5 0 00-6.364-6.364l-4.5 4.5a4.5 4.5 0 001.242 7.244"
            />
          </svg>
        </IconButton>
      </Tooltip>
      <ModalPrimary
        show={show}
        onClose={() => {
          setShow(false)
          setFile(null)
        }}
      >
        <div className="max-w-[560px] w-full p-4">
          <UpdateCover onFileChange={onFileChange} imageUrl={file || ""} />
          <CustomButton
            className="w-full mt-3"
            onClick={handleUploadImage}
            isLoading={createImageMutation.isLoading}
          >
            Upload Image
          </CustomButton>
        </div>
      </ModalPrimary>
      <input
        onKeyDown={(e) => {
          if (e.key === "Enter") {
            handleSendMessage()
          }
        }}
        value={textInput}
        onChange={(e) => setTextInput(e.target.value)}
        type="text"
        className="w-full px-3 py-2 my-auto transition-all bg-transparent bg-gray-100 border-none rounded-md outline-none placeholder:text-disable text-grayPrimary focus:ring-2 focus:ring-primary focus:ring-opacity-70"
        placeholder="Type a message"
      />
      <div
        className={classNames(
          "flex-shrink-0 flex items-center justify-center w-10 h-10 rounded-full bg-primary transition-all",
          (!textInput || isLoading) &&
            "bg-carbon bg-opacity-50 cursor-none pointer-events-none"
        )}
      >
        <IconButton
          size="small"
          className="text-white"
          disabled={!textInput || isLoading}
          onClick={() => handleSendMessage()}
        >
          <HiOutlinePaperAirplane />
        </IconButton>
      </div>
    </div>
  )
}

export default InputMessage
