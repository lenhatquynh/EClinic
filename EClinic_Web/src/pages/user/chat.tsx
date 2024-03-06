import ProfileLayout from "layout/User/ProfileLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const ChatData = dynamic(
  () => import("module/User/Profile/section/chat/ChatData"),
  {
    ssr: false
  }
)

const Page: NextPageWithLayout = () => {
  return <ChatData />
}
Page.getLayout = (page) => {
  return <ProfileLayout>{page}</ProfileLayout>
}
export default Page
