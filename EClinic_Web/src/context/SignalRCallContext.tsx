import * as signalR from "@microsoft/signalr"
import React, {
  PropsWithChildren,
  useContext,
  useEffect,
  useRef,
  useState
} from "react"
import { token } from "shared/utils/token"
import Peer from "simple-peer"
interface ICall {
  isReceivingCall: boolean
  roomId: string
  name: string
  signal: string
  userId: string
}
type TypeAnswerCall = "reject" | "accept"
interface ISignalRContext {
  signalRConnection: React.MutableRefObject<signalR.HubConnection | null>
  //state of call
  callAccepted: boolean
  callEnded: boolean
  isWatting: boolean
  isConnected: boolean
  isMicOn: boolean
  isVideoOn: boolean
  // action of call
  callUser: (roomId: string, userName: string, userId: string) => void
  answerCall: (type: TypeAnswerCall) => void
  leaveCall: (roomId: string) => void
  toggleMic: () => void
  toggleVideo: () => void
  myVideo: React.MutableRefObject<HTMLVideoElement | null>
  userVideo: React.MutableRefObject<HTMLVideoElement | null>
  stream?: MediaStream
  userStream?: MediaStream
  call: ICall
}

const initalCall = {
  isReceivingCall: false,
  roomId: "",
  name: "",
  signal: "",
  userId: ""
}
const SignalCallRContext = React.createContext<ISignalRContext | null>(null)
const SignalRCallContextProvider = ({ children }: PropsWithChildren<{}>) => {
  //call state
  const [isWatting, setIsWatting] = useState(false)
  const [callAccepted, setCallAccepted] = useState(false)
  const [callEnded, setCallEnded] = useState(true)
  const [isConnected, setIsConnected] = useState(false)
  const [isMicOn, setIsMicOn] = useState(true)
  const [isVideoOn, setIsVideoOn] = useState(true)

  const signalRConnection = useRef<signalR.HubConnection | null>(null)
  const connectionRef = useRef<Peer.Instance | null>(null)
  const [call, setCall] = useState(initalCall)
  const [stream, setStream] = useState<MediaStream>()
  const streamRef = useRef<MediaStream>()
  const userStreamRef = useRef<MediaStream>()
  const [userStream, setUserStream] = useState<MediaStream>()
  const myVideo = useRef<HTMLVideoElement | null>(null)
  const userVideo = useRef<HTMLVideoElement | null>(null)
  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(
        `${process.env.NEXT_PUBLIC_API_MAIN_URL}:${process.env.NEXT_PUBLIC_API_URL_PORT_SIGNALR}/call`,
        {
          accessTokenFactory: () => token.getToken().access_token || ""
        }
      )
      .withAutomaticReconnect()
      .build()
    connection
      .start()
      .then(() => {
        handleStart()
        console.log("SignalR call started.")
      })
      .catch((err) => {
        return console.error(err.toString())
      })
    const handleStart = () => {
      setIsConnected(true)
    }

    const handleStop = () => {
      setIsConnected(false)
    }

    connection.onclose(handleStop)
    connection.onreconnecting(handleStop)
    connection.onreconnected(handleStart)
    signalRConnection.current = connection
    signalRConnection.current.on("CallUser", (signal, data) => {
      const newData = JSON.parse(data)
      console.log("signalRConnection.current.on ~ newData:", newData)
      setCall({
        isReceivingCall: true,
        roomId: newData.roomId,
        name: newData.name,
        signal: JSON.parse(signal),
        userId: newData.userId
      })
    })
    signalRConnection.current.on("Disconnect", () => {
      streamRef.current!.getTracks().forEach((track) => track.stop())
      userStreamRef.current!.getTracks().forEach((track) => track.stop())
      closeConnection()
    })
    return () => {
      signalRConnection.current?.stop()
    }
  }, [])
  const callUser = (roomId: string, userName: string, userId: string) => {
    setCallEnded(false)
    setIsWatting(true)
    navigator.mediaDevices
      .getUserMedia({ video: true, audio: true })
      .then((currentStream) => {
        console.log(".then ~ currentStream:", currentStream)
        setStream(currentStream)
        streamRef.current = currentStream
        const peer = new Peer({
          initiator: true,
          trickle: false,
          stream: currentStream
        })
        peer.on("signal", (signal) => {
          const data = {
            roomId,
            name: userName,
            userId
          }
          signalRConnection
            .current!.invoke(
              "CallUser",
              JSON.stringify(signal),
              JSON.stringify(data),
              roomId
            )
            .catch((err) => {
              console.log("peer.on ~ err:", err)
            })
        })

        peer.on("stream", (currentStream) => {
          setUserStream(currentStream)
          console.log("get Stream ", currentStream)
          userStreamRef.current = currentStream
        })

        signalRConnection.current?.on(
          "AnswerCall",
          (signal, type: TypeAnswerCall) => {
            if (type === "accept") {
              const newSignal = JSON.parse(signal)
              peer.signal(newSignal)
              setCallAccepted(true)
              setIsWatting(false)
            } else {
              if (streamRef.current) {
                streamRef.current!.getTracks().forEach((track) => track.stop())
              }
              closeConnection()
              setCallAccepted(false)
              setCallEnded(true)
              setIsWatting(false)
            }
          }
        )
        connectionRef.current! = peer
      })
  }
  const answerCall = (type: TypeAnswerCall) => {
    if (type === "reject") {
      signalRConnection.current!.invoke("AnswerCall", "", type, call.roomId)
      closeConnection()
      setCallAccepted(false)
      setCallEnded(true)
      return
    } else {
      setCallAccepted(true)
      setCallEnded(false)

      navigator.mediaDevices
        .getUserMedia({ video: true, audio: true })
        .then((currentStream) => {
          setStream(currentStream)
          const peer = new Peer({
            initiator: false,
            trickle: false,
            stream: currentStream
          })
          streamRef.current = currentStream
          peer.on("signal", (signal) => {
            signalRConnection.current!.invoke(
              "AnswerCall",
              JSON.stringify(signal),
              type,
              call.roomId
            )
          })
          peer.on("stream", (currentStream) => {
            setUserStream(currentStream)
            userStreamRef.current = currentStream
          })
          peer.signal(call.signal)
          connectionRef.current! = peer
        })
    }
  }
  const leaveCall = (roomId: string) => {
    setIsWatting(false)
    closeConnection()
    if (streamRef.current) {
      streamRef.current!.getTracks().forEach((track) => track.stop())
    }
    signalRConnection.current!.invoke("Disconnect", roomId).catch((err) => {
      console.log("Disconnect error:", err)
    })
  }
  const closeConnection = () => {
    // End the call
    setCallEnded(true)
    setCall(initalCall)
    setCallAccepted(false)
    setIsMicOn(true)
    setIsVideoOn(true)
    // Clean up the Peer instance
    if (connectionRef.current) {
      connectionRef.current.destroy()
      connectionRef.current = null
    }
  }
  const toggleMic = () => {
    setIsMicOn((prevIsMicOn) => {
      const updatedIsMicOn = !prevIsMicOn
      if (streamRef.current) {
        const audioTrack = streamRef.current.getAudioTracks()[0]
        audioTrack.enabled = updatedIsMicOn
      }
      return updatedIsMicOn
    })
  }

  const toggleVideo = () => {
    setIsVideoOn((prevIsVideoOn) => {
      const updatedIsVideoOn = !prevIsVideoOn
      if (streamRef.current) {
        const videoTrack = streamRef.current.getVideoTracks()[0]
        videoTrack.enabled = updatedIsVideoOn
      }
      return updatedIsVideoOn
    })
  }

  return (
    <SignalCallRContext.Provider
      value={{
        signalRConnection,
        isWatting,
        answerCall,
        call,
        callAccepted,
        callEnded,
        callUser,
        leaveCall,
        myVideo,
        userVideo,
        stream,
        userStream,
        isConnected,
        isMicOn,
        isVideoOn,
        toggleMic,
        toggleVideo
      }}
    >
      {children}
    </SignalCallRContext.Provider>
  )
}

export const useSignalRCall = () => {
  const context = useContext(SignalCallRContext)
  if (context === null) {
    throw new Error(
      "useSignalR must be used within a SignalRCallContextProvider"
    )
  }
  return context
}

export { SignalRCallContextProvider }
