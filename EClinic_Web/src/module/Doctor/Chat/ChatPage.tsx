import ContainerChat from "components/Common/Chat/ContainerChat"
import EmtyData from "components/Common/Empty"
import { useGetAllRoomOfDoctorQuery } from "hooks/query/chat/room"
import MainHeadingLayout from "layout/Management/MainHeadingLayout"
import Head from "next/head"
import { useState } from "react"

const ChatPage = () => {
  const [pageIndex, setPageIndex] = useState(1)

  const { data, isLoading, isError } = useGetAllRoomOfDoctorQuery(
    pageIndex,
    100
  )
  if (isError) {
    return <EmtyData />
  }
  return (
    <>
      <Head>
        <title>Chat</title>
      </Head>
      <MainHeadingLayout heading="Chat with patient">
        <ContainerChat data={data?.data.data || []} isLoading={isLoading} />
      </MainHeadingLayout>
    </>
  )
}

export default ChatPage
