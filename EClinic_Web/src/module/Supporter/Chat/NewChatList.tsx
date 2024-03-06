import ListHistory from "components/Common/Chat/ListHistory"
import { useSignalRNotification } from "context/SignalRNotification"
import { useCreateChatMessage } from "hooks/query/chat/message"
import { useGetAllNewRoomQuery } from "hooks/query/chat/room"
import { useRouter } from "next/router"
import { queryClient } from "pages/_app"
import { useEffect, useState } from "react"
import { toast } from "react-hot-toast"
import { QUERY_KEYS } from "shared/constant/constant"
import { IRoom } from "types/Chat"

const NewChatList = () => {
  const [newChat, setNewChat] = useState<IRoom[]>([])
  const notification = useSignalRNotification()
  const router = useRouter()
  const [pageIndex, _] = useState(1)
  const createMessage = useCreateChatMessage()

  const newRoom = useGetAllNewRoomQuery(pageIndex, 100)

  const onClickHistory = (roomId: string) => {
    if (roomId) {
      createMessage.mutate(
        {
          roomId,
          content: "Hello, I'm supporter. How can I help you?"
        },
        {
          onSuccess: () => {
            queryClient.refetchQueries([QUERY_KEYS.CHAT.ROOM])
            router.push({
              pathname: router.pathname,
              query: {
                roomId
              }
            })
            setNewChat(newChat.filter((item) => item.roomID !== roomId))
          },
          onError: () => {
            toast.error("Create message fail")
          }
        }
      )
    }
  }
  useEffect(() => {
    if (notification?.newChat) {
      setNewChat([notification.newChat, ...newChat])
    }
  }, [notification?.newChat])

  const newChatHistory = newRoom.data?.data.data || []
  return (
    <>
      <ListHistory
        onClickHistory={onClickHistory}
        title="New messages"
        isLoading={newRoom.isLoading}
        data={[...newChat, ...newChatHistory] || []}
      />
    </>
  )
}

export default NewChatList
