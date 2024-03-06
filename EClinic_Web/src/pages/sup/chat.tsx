import SupporterLayout from "layout/Management/SupporterLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const ChatPage = dynamic(() => import("module/Supporter/Chat/ChatPage"), {
  ssr: false
})
const Page: NextPageWithLayout = () => {
  return <ChatPage />
}
Page.getLayout = (page) => {
  return <SupporterLayout>{page}</SupporterLayout>
}

export default Page
