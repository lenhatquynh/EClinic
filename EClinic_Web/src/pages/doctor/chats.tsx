import DoctorLayout from "layout/Management/DoctorLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const ChatPage = dynamic(() => import("module/Doctor/Chat/ChatPage"), {
  ssr: false
})
const Page: NextPageWithLayout = () => {
  return <ChatPage />
}
Page.getLayout = (page) => {
  return <DoctorLayout>{page}</DoctorLayout>
}

export default Page
