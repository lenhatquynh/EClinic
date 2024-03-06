import { Skeleton } from "@mui/material"
import classNames from "classnames"
import ImageCustom from "components/Common/ImageCustom"
import dayjs from "dayjs"
import { Message } from "types/Chat"
interface IProps {
  kind?: "owner" | "other"
  message?: Message
  isLoading?: boolean
  avatar: string
  isImage?: boolean
}
const TextMessage = ({
  kind = "other",
  message,
  isLoading = false,
  avatar,
  isImage
}: IProps) => {
  return (
    <div
      className={classNames(
        "flex items-end gap-x-2",
        kind === "owner" && "flex-row-reverse"
      )}
    >
      {isLoading ? (
        <Skeleton
          variant="circular"
          width={40}
          height={40}
          className="flex-shrink-0"
        />
      ) : (
        <div className="relative w-10 h-10 overflow-hidden rounded-full">
          <ImageCustom
            src={avatar}
            fill
            alt="user-avatar"
            className="object-cover"
          />
        </div>
      )}

      <div
        className={classNames(
          "flex items-center gap-x-2",
          kind === "owner" && "flex-row-reverse"
        )}
      >
        <div className="flex flex-col">
          <time className="text-xs text-disable">
            {isLoading ? (
              <Skeleton variant="text" width={60} />
            ) : (
              dayjs(message?.createdAt).format("DD MMM YYYY HH:mm")
            )}
          </time>

          {isLoading ? (
            <Skeleton variant="text" width={260} height={40} />
          ) : isImage ? (
            <div className="relative w-[220px] h-[200px]">
              <ImageCustom
                src={message?.content as string}
                alt="image"
                fill
                className="rounded-md"
              />
            </div>
          ) : (
            <div
              className={classNames(
                "max-w-[266px] rounded-xl py-3 text-sm p-4",
                kind === "other"
                  ? "bg-gray-100"
                  : "bg-primary bg-opacity-80 text-white"
              )}
            >
              <p className="break-words break-all whitespace-normal">
                {message?.content}
              </p>
            </div>
          )}
        </div>
      </div>
    </div>
  )
}

export default TextMessage
