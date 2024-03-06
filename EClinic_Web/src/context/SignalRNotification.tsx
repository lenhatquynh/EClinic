import * as signalR from "@microsoft/signalr"
import dayjs from "dayjs"
import React, { PropsWithChildren, useContext, useEffect, useRef } from "react"
import { IRoom, Message, ProfileChat } from "types/Chat"

interface ISignalRContext {
  connectionNotification: React.MutableRefObject<signalR.HubConnection | null>
  isConnected: boolean
  newChat: IRoom | null
}

const SignalNotificationRContext = React.createContext<ISignalRContext | null>(
  null
)

const SignalRNotificationContextProvider = ({
  children
}: PropsWithChildren<{}>) => {
  const [newChat, setNewChat] = React.useState<IRoom | null>(null)
  const connectionNotification = useRef<signalR.HubConnection | null>(null)
  const [isConnected, setIsConnected] = React.useState(false)
  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(
        `${process.env.NEXT_PUBLIC_API_MAIN_URL}:${process.env.NEXT_PUBLIC_API_URL_PORT_SIGNALR}/notification`
      )
      .withAutomaticReconnect()
      .build()
    setIsConnected(true)
    connectionNotification.current = connection
    connection!.start()
    connection.on(
      "Response",
      (profile: ProfileChat, message: Message, roomId: string) => {
        setNewChat({
          roomID: roomId,
          chatMessage: message,
          createdAt: dayjs().toDate().toString(),
          roomAuthor: profile,
          roomType: {
            roomTypeID: "",
            roomTypeName: ""
          },
          isClosed: false
        })
      }
    )
    return () => {
      connection?.stop()
    }
  }, [])

  return (
    <SignalNotificationRContext.Provider
      value={{ connectionNotification, isConnected, newChat }}
    >
      {children}
    </SignalNotificationRContext.Provider>
  )
}

export const useSignalRNotification = () => {
  const context = useContext(SignalNotificationRContext)
  if (context === null) {
    return null
  }
  return context
}

export { SignalRNotificationContextProvider }
