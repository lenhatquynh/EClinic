import React, { PropsWithChildren, useContext, useEffect, useRef } from "react"
import * as signalR from "@microsoft/signalr"
import { token } from "shared/utils/token"

interface ISignalRContext {
  connectionMessage: React.MutableRefObject<signalR.HubConnection | null>
  isConnected: boolean
}

const SignalMessageRContext = React.createContext<ISignalRContext | null>(null)

const SignalRMessageContextProvider = ({ children }: PropsWithChildren<{}>) => {
  const connectionMessage = useRef<signalR.HubConnection | null>(null)
  const [isConnected, setIsConnected] = React.useState(false)
  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(
        `${process.env.NEXT_PUBLIC_API_MAIN_URL}:${process.env.NEXT_PUBLIC_API_URL_PORT_SIGNALR}/message`,
        {
          accessTokenFactory: () => token.getToken().access_token || ""
        }
      )
      .withAutomaticReconnect()
      .build()
    const handleStart = () => {
      setIsConnected(true)
    }

    const handleStop = () => {
      setIsConnected(false)
    }

    connection.onclose(handleStop)
    connection.onreconnecting(handleStop)
    connection.onreconnected(handleStart)

    connection
      .start()
      .then(handleStart)
      .catch((err) => {
        console.error(err.toString())
        setIsConnected(false)
      })

    connectionMessage.current = connection

    return () => {
      connectionMessage.current?.stop()
    }
  }, [])

  return (
    <SignalMessageRContext.Provider value={{ connectionMessage, isConnected }}>
      {children}
    </SignalMessageRContext.Provider>
  )
}

export const useSignalRMessage = () => {
  const context = useContext(SignalMessageRContext)
  if (context === null) {
    throw new Error("useSignalR must be used within a SignalRContextProvider")
  }
  return context
}

export { SignalRMessageContextProvider }
