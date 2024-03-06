import { IconButton, Tooltip } from "@mui/material"
import classNames from "classnames"
import ImageCustom from "components/Common/ImageCustom"
import { useSignalRCall } from "context/SignalRCallContext"
import { useRouter } from "next/router"
import { useEffect, useState } from "react"
import { useDispatch } from "react-redux"
import { combineName } from "shared/helpers/helper"
import { headerSlice } from "store/module/header/header-slice"
import { ProfileChat } from "types/Chat"
interface Props {
  userProfile?: ProfileChat
  otherProfile?: ProfileChat
}
const VideoCall = ({ userProfile, otherProfile }: Props) => {
  const { query } = useRouter()
  const roomId = query.roomId as string

  const [isFullScreen, setIsFullScreen] = useState(false)
  const {
    stream,
    userStream,
    myVideo,
    userVideo,
    callAccepted,
    callEnded,
    leaveCall,
    isWatting,
    toggleMic,
    toggleVideo,
    isMicOn,
    isVideoOn
  } = useSignalRCall()
  useEffect(() => {
    if (stream) {
      myVideo.current!.srcObject = stream
    }
  }, [myVideo, stream])
  useEffect(() => {
    if (userStream && userVideo.current) {
      userVideo.current!.srcObject = userStream
    }
  }, [userStream, callAccepted, callEnded, userVideo])
  const dispatch = useDispatch()
  const handleChangeSizeVideo = () => {
    dispatch(headerSlice.actions.onChangeZIndex(isFullScreen ? 50 : 0))
    setIsFullScreen(!isFullScreen)
  }
  return (
    <>
      <div className={classNames(callEnded ? "p-0" : "p-4")}>
        <div
          className={classNames(
            "bg-black transition-all",
            isFullScreen
              ? "fixed  z-50"
              : "relative w-full mx-auto overflow-hidden rounded-xl ",
            callEnded
              ? "h-0 opacity-0"
              : isFullScreen
              ? "inset-0"
              : "h-96 opacity-100"
          )}
        >
          <Tooltip
            title="Full screen"
            placement="top"
            className="absolute top-0 left-0 z-50"
          >
            <IconButton onClick={() => handleChangeSizeVideo()}>
              <div className="flex items-center justify-center p-2 text-white transition-opacity border-none rounded-full outline-none cursor-pointer opacity-40 hover:opacity-100 bg-gray80">
                <svg
                  width={24}
                  height={24}
                  viewBox="0 0 24 24"
                  fill="none"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path
                    d="M4 16.6667H7.33333V20H9.33333V14.6667H4V16.6667ZM7.33333 7.33333H4V9.33333H9.33333V4H7.33333V7.33333ZM14.6667 20H16.6667V16.6667H20V14.6667H14.6667V20ZM16.6667 7.33333V4H14.6667V9.33333H20V7.33333H16.6667Z"
                    fill="white"
                  />
                </svg>
              </div>
            </IconButton>
          </Tooltip>
          <video
            className="absolute top-0 right-0 z-50 object-cover w-32 h-40 overflow-hidden -translate-x-4 translate-y-4 rounded-xl"
            ref={myVideo}
            playsInline
            autoPlay
            muted
          />
          <video
            className="absolute object-contain w-full h-auto max-w-3xl left-2/4 -translate-x-2/4 top-2/4 -translate-y-2/4"
            ref={userVideo}
            playsInline
            autoPlay
          />
          {isWatting && (
            <div className="absolute flex flex-col items-center transform -translate-x-1/2 -translate-y-1/2 top-1/2 left-1/2 gap-y-2">
              <div className="relative w-24 h-24">
                <ImageCustom
                  src={otherProfile?.avatar || "/images/avatars/avatar_1.jpg"}
                  fill
                  alt="avatar-user"
                  className="rounded-full"
                />
              </div>
              <h3 className="text-2xl font-semibold text-white">
                {combineName(otherProfile?.firstName, otherProfile?.lastName)}
              </h3>
              <span className="text-base text-disable">Ringing...</span>
            </div>
          )}
          <div className="absolute bottom-0 z-20 flex items-center justify-center w-full p-4 bg-white bg-opacity-10">
            <Tooltip
              title={isVideoOn ? "Turn off camera" : "Turn on camera"}
              placement="top"
            >
              <IconButton onClick={toggleVideo}>
                <div className="flex items-center justify-center p-2 text-white border-none rounded-full outline-none cursor-pointer bg-gray80">
                  {isVideoOn ? (
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      fill="none"
                      viewBox="0 0 24 24"
                      strokeWidth={1.5}
                      stroke="currentColor"
                      className="w-5 h-5"
                    >
                      <path
                        strokeLinecap="round"
                        d="M15.75 10.5l4.72-4.72a.75.75 0 011.28.53v11.38a.75.75 0 01-1.28.53l-4.72-4.72M4.5 18.75h9a2.25 2.25 0 002.25-2.25v-9a2.25 2.25 0 00-2.25-2.25h-9A2.25 2.25 0 002.25 7.5v9a2.25 2.25 0 002.25 2.25z"
                      />
                    </svg>
                  ) : (
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      fill="none"
                      viewBox="0 0 24 24"
                      strokeWidth={1.5}
                      stroke="currentColor"
                      className="w-5 h-5"
                    >
                      <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        d="M15.75 10.5l4.72-4.72a.75.75 0 011.28.53v11.38a.75.75 0 01-1.28.53l-4.72-4.72M12 18.75H4.5a2.25 2.25 0 01-2.25-2.25V9m12.841 9.091L16.5 19.5m-1.409-1.409c.407-.407.659-.97.659-1.591v-9a2.25 2.25 0 00-2.25-2.25h-9c-.621 0-1.184.252-1.591.659m12.182 12.182L2.909 5.909M1.5 4.5l1.409 1.409"
                      />
                    </svg>
                  )}
                </div>
              </IconButton>
            </Tooltip>
            <Tooltip title="End session" placement="top">
              <IconButton onClick={() => leaveCall(roomId)}>
                <div className="flex items-center justify-center p-3 text-white border-none rounded-md outline-none cursor-pointer bg-error">
                  <svg
                    width={18}
                    height={18}
                    viewBox="0 0 18 18"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      d="M3.02742 11.6618L1.82648 10.5029C1.36554 10.0618 1.39497 9.29356 1.90027 8.88666C5.89753 5.02358 12.131 5.05138 16.1014 8.88856C16.6042 9.29313 16.6339 10.058 16.1759 10.5008L14.9726 11.6619C14.7798 11.8516 14.5223 11.9673 14.248 11.9875C13.4682 12.0449 11.9759 11.1258 11.5938 10.5566C11.2955 10.1123 11.4125 9.48714 11.413 8.98584C9.8398 8.56976 8.17854 8.57135 6.60448 8.99043C6.60397 9.49174 6.71971 10.1167 6.42046 10.5616C6.03659 11.1323 4.536 12.0587 3.75475 11.9971C3.47772 11.9752 3.21886 11.8559 3.02742 11.6618Z"
                      stroke="white"
                      strokeWidth="1.5"
                      strokeLinecap="round"
                      strokeLinejoin="round"
                    />
                  </svg>
                </div>
              </IconButton>
            </Tooltip>
            <Tooltip
              title={isMicOn ? "Turn off microphone" : "Turn on microphone"}
              placement="top"
            >
              <IconButton onClick={toggleMic}>
                <div className="flex items-center justify-center p-2 text-white border-none rounded-full outline-none cursor-pointer bg-gray80">
                  {isMicOn ? (
                    <svg
                      width={18}
                      height={18}
                      viewBox="0 0 12 18"
                      fill="none"
                      xmlns="http://www.w3.org/2000/svg"
                    >
                      <path
                        d="M11.25 9C11.25 11.7614 8.89949 14 6 14M6 14C3.10051 14 0.75 11.7614 0.75 9M6 14V16.5M6 16.5H8.625M6 16.5H3.375M6 11.5C4.55025 11.5 3.375 10.3807 3.375 9V4C3.375 2.61929 4.55025 1.5 6 1.5C7.44975 1.5 8.625 2.61929 8.625 4V9C8.625 10.3807 7.44975 11.5 6 11.5Z"
                        stroke="white"
                        strokeWidth="1.2"
                        strokeLinecap="round"
                        strokeLinejoin="round"
                      />
                    </svg>
                  ) : (
                    <svg
                      width={18}
                      height={18}
                      viewBox="0 0 24 24"
                      fill="none"
                      xmlns="http://www.w3.org/2000/svg"
                    >
                      <path
                        d="M15.3333 10.3333V5.33333C15.3333 3.49238 13.8409 2 12 2C10.5486 2 9.31394 2.92756 8.85634 4.22222M12 18.6667C8.3181 18.6667 5.33333 15.6819 5.33333 12M12 18.6667C12.5755 18.6667 13.134 18.5937 13.6667 18.4566M12 18.6667V22M18.6667 12C18.6667 12.5755 18.5937 13.134 18.4566 13.6667M12 22H15.3333M12 22H8.66667M2 2L22 22"
                        stroke="#5F666F"
                        strokeWidth="1.5"
                        strokeLinecap="round"
                        strokeLinejoin="round"
                      />
                    </svg>
                  )}
                </div>
              </IconButton>
            </Tooltip>
          </div>
        </div>
      </div>
    </>
  )
}

export default VideoCall
