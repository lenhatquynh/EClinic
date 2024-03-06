import { SignalRCallContextProvider } from "context/SignalRCallContext"
import { SignalRMessageContextProvider } from "context/SignalRMessageContext"
import { useRouter } from "next/router"
import { useEffect, useState } from "react"
import { IRoom } from "types/Chat"
import EmtyData from "../Empty"
import ListHistory from "./ListHistory"
import MessageBox from "./MessageBox/MessageBox"
import UserProfile from "./UserProfile"
interface IProps {
  data: IRoom[]
  isLoading: boolean
}
const ContainerChat = ({ data, isLoading = true }: IProps) => {
  const { query } = useRouter()
  const roomId = query.roomId as string
  const [show, setShow] = useState(false)
  const [otherUserId, setOtherUserId] = useState<string | null>(null)
  useEffect(() => {
    if (roomId) {
      const author = data.find((item) => item.roomID === roomId)
      setOtherUserId(author?.roomAuthor.userID!)
    }
  }, [data, roomId])
  return (
    <SignalRMessageContextProvider>
      <SignalRCallContextProvider>
        <div className="flex p-0 background-primary h-[400px] md:h-[600px]">
          <ListHistory isLoading={isLoading} data={data} />
          {roomId && otherUserId ? (
            <MessageBox
              isClose={
                data.find((room) => room.roomID === roomId)?.isClosed || false
              }
              key={roomId}
              otherUserId={otherUserId}
              toggleInfo={() => setShow(!show)}
            />
          ) : (
            <EmtyData
              message="Select a chat to conversation"
              className="flex-1 max-auto"
            />
          )}
          {show && roomId && otherUserId && (
            <UserProfile
              roomId={roomId}
              userId={otherUserId!}
              onClose={() => setShow(!show)}
            />
          )}
        </div>
      </SignalRCallContextProvider>
    </SignalRMessageContextProvider>
  )
}

export default ContainerChat
