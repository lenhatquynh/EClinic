import SupporterLayout from "layout/Management/SupporterLayout"
import dynamic from "next/dynamic"
import { NextPageWithLayout } from "pages/page"
const AppointmentPage = dynamic(
  () => import("module/Supporter/Appointment/AppointmentPage"),
  {
    ssr: false
  }
)
const Page: NextPageWithLayout = () => {
  return <AppointmentPage />
}
Page.getLayout = (page) => {
  return <SupporterLayout>{page}</SupporterLayout>
}

export default Page
