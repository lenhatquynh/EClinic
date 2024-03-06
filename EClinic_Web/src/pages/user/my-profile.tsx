import ProfileLayout from "layout/User/ProfileLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const Profile = dynamic(() => import("module/User/Profile/section/profile"), {
  ssr: false
})

const Page: NextPageWithLayout = () => {
  return <Profile />
}
Page.getLayout = (page) => {
  return <ProfileLayout>{page}</ProfileLayout>
}
export default Page
