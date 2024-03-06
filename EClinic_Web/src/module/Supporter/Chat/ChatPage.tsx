import ContainerChat from "components/Common/Chat/ContainerChat"
import EmtyData from "components/Common/Empty"
import { SignalRNotificationContextProvider } from "context/SignalRNotification"
import { useGetAllRoomOfSupporterQuery } from "hooks/query/chat/room"
import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import Head from "next/head"
import { useState } from "react"
import NewChatList from "./NewChatList"

const ChatPage = () => {
  const [pageIndex, setPageIndex] = useState(1)

  const { data, isLoading, isError } = useGetAllRoomOfSupporterQuery(
    pageIndex,
    100
  )
  if (isError) {
    return <EmtyData />
  }
  return (
    <>
      <Head>
        <title>Support list</title>
      </Head>
      <SignalRNotificationContextProvider>
        <MainHeadingLayout heading="List of all messages that need support">
          <div className="flex gap-x-4 min-h-[600px]">
            <div className="flex flex-col rounded-lg shadow-[rgba(145,_158,_171,_0.2)_0px_0px_2px_0px,_rgba(145,_158,_171,_0.12)_0px_12px_24px_-4px] bg-gray-50 max-h-[600px] overflow-y-auto">
              <NewChatList />
            </div>
            <div className="flex-1">
              <ContainerChat
                data={data?.data.data || []}
                isLoading={isLoading}
              />
            </div>
          </div>
        </MainHeadingLayout>
      </SignalRNotificationContextProvider>
    </>
  )
}

export default ChatPage
