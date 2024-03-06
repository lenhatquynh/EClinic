import { IconButton, Popover, Tooltip } from "@mui/material"
import { useMutation } from "@tanstack/react-query"
import ImageCustom from "components/Common/ImageCustom"
import { PortalCustom } from "components/Common/Portal/PortalCustom"
import TextAreaCustom from "components/Common/Textarea/TextAreaCustom"
import CustomButton from "components/User/Button"
import { useRouter } from "next/router"
import React from "react"
import { toast } from "react-hot-toast"
import { useTranslation } from "react-i18next"
import { chatService } from "services/chat.service"

const ChatSection = () => {
  const { t } = useTranslation("forum")
  const router = useRouter()
  const [value, setValue] = React.useState<string>("")
  const [anchorEl, setAnchorEl] = React.useState<HTMLButtonElement | null>(null)
  const createRoom = useMutation(chatService.CreateSupporterRoom)
  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget)
  }

  const handleClose = () => {
    setAnchorEl(null)
  }
  const handleCreateRoom = () => {
    if (value.trim()) {
      createRoom.mutate(value, {
        onSuccess: (data) => {
          router.push(`/user/chat?roomId=${data?.data}`)
        },
        onError: (error: any) => {
          toast.error(error?.response?.data?.message || "Failed to create chat")
        }
      })
    } else toast.error("Please enter your question")
  }
  const open = Boolean(anchorEl)
  return (
    <PortalCustom isActive={true}>
      <div className="fixed right-[23px] bottom-[23px]">
        <Tooltip title="Chat with supporter">
          <IconButton onClick={handleClick}>
            <div className="p-3 text-white rounded-full shadow-md bg-primary">
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
                  d="M9.879 7.519c1.171-1.025 3.071-1.025 4.242 0 1.172 1.025 1.172 2.687 0 3.712-.203.179-.43.326-.67.442-.745.361-1.45.999-1.45 1.827v.75M21 12a9 9 0 11-18 0 9 9 0 0118 0zm-9 5.25h.008v.008H12v-.008z"
                />
              </svg>
            </div>
          </IconButton>
        </Tooltip>
        <Popover
          open={open}
          anchorEl={anchorEl}
          onClose={handleClose}
          anchorOrigin={{
            vertical: "top",
            horizontal: "left"
          }}
          transformOrigin={{
            vertical: "bottom",
            horizontal: "center"
          }}
        >
          <div className="relative flex flex-col w-[360px] gap-y-2 pb-3">
            <div className="relative w-full h-[200px]">
              <ImageCustom
                src={"/images/hello-chat.png"}
                fill
                alt="chat"
                className="object-cover"
              />
            </div>
            <div className="flex flex-col gap-2">
              <h3 className="text-2xl text-h1">{t("chatTitle")}</h3>
              <TextAreaCustom
                value={value}
                onChange={(e) => {
                  setValue(e.target.value)
                }}
                classCustom="max-w-full h-[120px]"
                className="resize-none"
                placeholder={t("textareaChat")}
              />
            </div>
            <CustomButton onClick={handleCreateRoom}>Send</CustomButton>
          </div>
        </Popover>
      </div>
    </PortalCustom>
  )
}

export default ChatSection
