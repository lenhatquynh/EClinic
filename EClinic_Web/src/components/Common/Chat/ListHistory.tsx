import { IconButton, Skeleton, Tooltip } from "@mui/material"
import classNames from "classnames"
import { useRouter } from "next/router"
import { OverlayScrollbarsComponent } from "overlayscrollbars-react"
import { combineName, dayformat } from "shared/helpers/helper"
import { IRoom } from "types/Chat"
import ImageCustom from "../ImageCustom"
import { queryClient } from "pages/_app"
import { QUERY_KEYS } from "shared/constant/constant"
import { useSelector } from "react-redux"
import { RootState } from "store/store"
interface IProps {
  data: IRoom[]
  isLoading: boolean
  title?: string
  onClickHistory?: (roomId: string) => void
}
const ListHistory = ({
  data,
  isLoading = false,
  title = "Message",
  onClickHistory
}: IProps) => {
  const onReload = () => {
    queryClient.refetchQueries([QUERY_KEYS.CHAT.ROOM])
  }
  return (
    <div className="relative flex flex-col bg-gray-50 w-full max-w-[320px]">
      <div className="flex items-center justify-between px-5 py-3 ">
        <h1 className="text-xl text-h1">{title}</h1>
        <Tooltip title="Reload message list">
          <IconButton className="w-fit" onClick={onReload}>
            <div className="p-2 bg-gray-200 rounded-full">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                strokeWidth={1.5}
                stroke="currentColor"
                className="w-4 h-4"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  d="M16.023 9.348h4.992v-.001M2.985 19.644v-4.992m0 0h4.992m-4.993 0l3.181 3.183a8.25 8.25 0 0013.803-3.7M4.031 9.865a8.25 8.25 0 0113.803-3.7l3.181 3.182m0-4.991v4.99"
                />
              </svg>
            </div>
          </IconButton>
        </Tooltip>
      </div>
      <OverlayScrollbarsComponent
        defer
        options={{ scrollbars: { autoHide: "scroll" } }}
      >
        <div className="flex-1 space-y-4">
          {isLoading &&
            Array(6)
              .fill(0)
              .map((_, index) => (
                <HistoryItem isLoading={isLoading} key={index} />
              ))}
          {data.length > 0 &&
            data.map((item, index) => (
              <HistoryItem
                onClickHistory={onClickHistory}
                room={item}
                key={index}
              />
            ))}
        </div>
      </OverlayScrollbarsComponent>
    </div>
  )
}
interface HistoryProps {
  room?: IRoom
  isLoading?: boolean
  onClickHistory?: (roomId: string) => void
}
export const HistoryItem = ({
  room,
  isLoading = false,
  onClickHistory
}: HistoryProps) => {
  const userId = useSelector((state: RootState) => state.auth.user.userId)

  const router = useRouter()
  const roomId = router.query.roomId as string
  const onClickItem = () => {
    if (room) {
      if (onClickHistory) onClickHistory(room.roomID)
      else {
        router.push({
          pathname: router.pathname,
          query: {
            roomId: room.roomID
          }
        })
      }
    }
  }
  return (
    <div
      className={classNames(
        "flex gap-x-3 items-center bg-white px-5 py-[6px] cursor-pointer hover:bg-blue-100 transition-all",
        roomId === room?.roomID && "bg-carbon"
      )}
      onClick={onClickItem}
    >
      {isLoading && (
        <Skeleton
          variant="circular"
          width={40}
          height={40}
          className="flex-shrink-0"
        />
      )}
      {room?.roomAuthor && (
        <div className="relative flex-shrink-0 w-10 h-10">
          <ImageCustom
            src={room?.roomAuthor.avatar || "/images/avatars/avatar_2.jpg"}
            fill
            alt="user-avatar"
            className="object-cover rounded-full"
          />
        </div>
      )}

      <div className="flex flex-col justify-between flex-1">
        <div className="flex items-center justify-between">
          {isLoading && (
            <>
              <Skeleton variant="text" width={80} />
            </>
          )}
          <p className="text-sm font-medium text-h1 max-w-[110px] line-clamp-1">
            {room?.roomAuthor &&
              combineName(
                room?.roomAuthor.firstName,
                room?.roomAuthor?.lastName
              )}
          </p>
          <time className="text-xs align-top text-black2 ">
            {isLoading && <Skeleton variant="text" width={60} />}
            {room?.roomAuthor && dayformat(room?.createdAt)}
          </time>
        </div>
        <span className="text-xs text-disable line-clamp-1">
          {isLoading && <Skeleton variant="text" width={100} />}
          {room?.chatMessage.isImage
            ? userId === room.chatMessage.userID
              ? "You sent a photo"
              : `${room?.roomAuthor.firstName} sent a photo`
            : room?.chatMessage && room?.chatMessage.content}
        </span>
      </div>
    </div>
  )
}
export default ListHistory
