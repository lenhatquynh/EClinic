import ProfileLayout from "layout/User/ProfileLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const ChangePassword = dynamic(
  () => import("module/User/Profile/section/change-password"),
  {
    ssr: false
  }
)

const Page: NextPageWithLayout = () => {
  return <ChangePassword />
}
Page.getLayout = (page) => {
  return <ProfileLayout>{page}</ProfileLayout>
}
export default Page
