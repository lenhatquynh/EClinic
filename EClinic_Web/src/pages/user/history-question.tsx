import ProfileLayout from "layout/User/ProfileLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const HistoryQuestion = dynamic(
  () => import("module/User/Profile/section/history-question"),
  {
    ssr: false
  }
)

const Page: NextPageWithLayout = () => {
  return <HistoryQuestion />
}
Page.getLayout = (page) => {
  return <ProfileLayout>{page}</ProfileLayout>
}
export default Page
