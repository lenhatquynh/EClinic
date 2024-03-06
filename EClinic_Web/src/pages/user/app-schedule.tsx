import ProfileLayout from "layout/User/ProfileLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const AppointmentSchedule = dynamic(
  () => import("module/User/Profile/section/appoiment/AppointmentSchedule"),
  {
    ssr: false
  }
)

const Page: NextPageWithLayout = () => {
  return <AppointmentSchedule />
}
Page.getLayout = (page) => {
  return <ProfileLayout>{page}</ProfileLayout>
}
export default Page
